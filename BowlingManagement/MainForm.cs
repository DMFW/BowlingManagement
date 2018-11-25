using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BowlingManagement
{
  public partial class MainForm : Form
  {

    private SeasonValidator _workingSeasonValidator;

    public MainForm()
    {
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {

      BowlingManagement.Static.Logs.Status.TimeStampLog = true;
      BowlingManagement.Static.Logs.UpdateSQLLog.TimeStampLog = false;

      // Attach event listeners to the database control object and the logs so
      // we can reflect events into the form.
      Static.Database.DatabaseStateChange += this.OnDatabaseStateChange;
      BowlingManagement.Static.Logs.Status.LogMessage += this.OnStatusLogMessage;
      BowlingManagement.Static.Logs.UpdateSQLLog.LogMessage += this.OnSQLLogMessage;

      // And we're off...
      BowlingManagement.Static.Logs.Status.AddLogMessage("Program started", Log.MessageSeverity.Info);

      if (Properties.Settings.Default.ConnectionString != "")
      {
        txtConnectionString.Text = Properties.Settings.Default.ConnectionString;
      }
      else
      {
        txtConnectionString.Text = @"Server=127.0.0.1;Uid=root;Pwd=;Database=davlon0_bowla300;";
      }

      txtLogPath.Text = Properties.Settings.Default.StatusLogPath;
      txtSQLOutputPath.Text = Properties.Settings.Default.SQLLogPath;

    }

    private void OnStatusLogMessage(object sender, LogMessageArgs e)
    {
      AddMessageToListView(lvwStatus, e);
    }

    private void OnSQLLogMessage(object sender, LogMessageArgs e)
    {
      AddMessageToListView(lvwFixSQL, e);
    }

    private void AddMessageToListView(ListView lvwTargetListView, LogMessageArgs e)
    {
      ListViewItem lvi = new ListViewItem();
      Color rowColour;

      lvi.Text = e.Message;
      switch (e.Severity)
      {
        case Log.MessageSeverity.Error:
          rowColour = Color.Red;
          break;
        case Log.MessageSeverity.Warning:
          rowColour = Color.Orange;
          break;
        case Log.MessageSeverity.Info:
          rowColour = Color.Green;
          break;
        default:
          rowColour = Color.Black;
          break;
      }

      lvi.ForeColor = rowColour;

      if (lvwTargetListView.InvokeRequired)
      {
        lvwTargetListView.Invoke(new Action(() => lvwTargetListView.Items.Add(lvi)));
      }
      else
      {
        lvwTargetListView.Items.Add(lvi);
      }
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      Static.Database.Open(txtConnectionString.Text);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Static.Database.Close();
    }

    public void OnDatabaseStateChange(DatabaseStateChangeArgs args)
    {
      if (args.Open)
      {
        txtConnectionString.BackColor = Color.PaleGreen;
        btnLoadSeason.Enabled = true;
        btnValidateSeason.Enabled = false;
      }
      else
      {
        txtConnectionString.BackColor = Color.Salmon;
        btnLoadSeason.BackColor = Color.Transparent;
        btnLoadSeason.Enabled = false;
        btnValidateSeason.Enabled = false;
      }
    }

    private void btnLoadSeason_Click(object sender, EventArgs e)
    {
      if ((txtSeasonYear.Text.Trim() == "") & (chkCurrentSeason.CheckState != CheckState.Checked))
      {
        MessageBox.Show("A season year must be entered or the current season checked before loading");
      }
      else
      {
        bgwLoad.RunWorkerAsync();
      }
    }

    private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
    {
      if (chkClearLogs.CheckState == CheckState.Checked)
      {
        BowlingManagement.Static.Logs.Status.Clear();
        BowlingManagement.Static.Logs.UpdateSQLLog.Clear();
      }

      _workingSeasonValidator = new SeasonValidator();
      if (chkCurrentSeason.CheckState == CheckState.Checked)
      {
        _workingSeasonValidator.Load("Current");
      }
      else
      {
        _workingSeasonValidator.Load(txtSeasonYear.Text);
      }
    }

    private void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      btnLoadSeason.BackColor = Color.PaleGreen;
      btnValidateSeason.Enabled = true;
      txtSeasonYear.Text = _workingSeason.Parameters.Year;
    }

    private void btnValidateSeason_Click(object sender, EventArgs e)
    {
      DialogResult reply = MessageBox.Show(this, "Ready to validate season " + _workingSeason.Parameters.Year, "Bowling Management", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
      if (reply == DialogResult.Cancel) { return; }
      bgwValidate.RunWorkerAsync();
    }

    private void txtLogPath_TextChanged(object sender, EventArgs e)
    {
      Properties.Settings.Default.StatusLogPath = txtLogPath.Text;
    }

    private void txtSQLOutputPath_TextChanged(object sender, EventArgs e)
    {
      Properties.Settings.Default.SQLLogPath =txtSQLOutputPath.Text;
    }
    private void txtConnectionString_TextChanged(object sender, EventArgs e)
    {
      Properties.Settings.Default.ConnectionString = txtConnectionString.Text;
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      Properties.Settings.Default.Save();
    }

    private void bgwValidate_DoWork(object sender, DoWorkEventArgs e)
    {
      BowlingManagement.Static.Logs.Status.AddLogMessage("Validation processing started for " + _workingSeason.Parameters.Year, Log.MessageSeverity.Info);
      _workingSeason.Validate();
    }

    private void bgwValidate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      BowlingManagement.Static.Logs.Status.AddLogMessage("Validation processing completed for " + _workingSeason.Parameters.Year, Log.MessageSeverity.Info);
      MessageBox.Show("Season has been validated");
    }

    private void chkCurrentSeason_CheckedChanged(object sender, EventArgs e)
    {
      if (chkCurrentSeason.CheckState == CheckState.Checked)
      {
        // Set the season text to blank if we are forcing the current season
        txtSeasonYear.Text = "";
      }
    }
  }
}

