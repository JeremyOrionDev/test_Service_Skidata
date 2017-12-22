namespace SkiData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using SkiData.CP.FormatService.Client;
    using SkiData.CP.FormatService.Contracts;
    using SkiData.CP.FormatService.CustomerTestClient;
    using System.ServiceModel.Security;

    internal partial class Form1 : Form
    {
  
        /// <summary>
        /// Holds the formatting service wrapper.
        /// </summary>
        private FormattingServiceWrapper formatting;

        private string format;
    


        public Form1()
        {
            InitializeComponent();
            gBxGetData.Enabled = false;
            init();
            ConnectIfPossible();
        }

        /// <summary>
        /// Connects to the Format.Service if all parameters are available.
        /// </summary>
        private void ConnectIfPossible()
        {
            string serverName = this.toolStripTextBoxServer.Text;
            string userName = this.usernameTextBox.Text;
            string pwd = this.passwordTextBox.Text;

            if (!string.IsNullOrWhiteSpace(serverName) &&
                !string.IsNullOrWhiteSpace(userName) &&
                !string.IsNullOrWhiteSpace(pwd))
            {
                connect();
            }
        }

        private void init()
        {
            // password box in main toolstrip
            var passwordTextBox = this.passwordTextBox.Control as TextBox;
            passwordTextBox.PasswordChar = '*';

            // set credentials, if available
            this.toolStripTextBoxServer.Text = SettingsManagement.GetHostName();
            this.toolStripTextBoxCertificat.Text = SettingsManagement.GetClientCertificate();
        }
        
        private async void recupFormats()
        {
            var formats = await this.formatting.GetAvailableFormatTypesAsync();
            foreach (var item in formats)
            {
                this.cBxFormats.Items.Add(item.Name);
            }
        }

        public async void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            connect();

        }

        public async void connect()
        {
            SettingsManagement.PersistConnectionData(
                this.toolStripTextBoxServer.Text,
                this.toolStripTextBoxCertificat.Text);

            var uri =
                new Uri(
                    string.Format(
                        "https://{0}:{1}/",
                        SettingsManagement.GetHostName(),
                        SettingsManagement.GetServicePort()));

            formatting = new FormattingServiceWrapper();

            try
            {
                // path to service certificate (located in program folder)
                var serverCertPath =@"C:\CERTIFICAT\Format.Service_server.cer";

                var svcConfig =
                    new FormattingServiceConfig(
                        uri,
                        serverCertPath,
                        SettingsManagement.GetClientCertificate());
                formatting.Initialize(svcConfig);

                // get version, for testing
                this.SwitchWaiting(true);
                string version = await formatting.GetVersionAsync();
                this.SwitchOnline(true, version);
                recupFormats();
            }
            catch (Exception ex)
            {
                this.HandleFailure(ex);
            }
            finally
            {
                this.SwitchWaiting(false);
            }
        }

        /// <summary>
        /// Handles failed web service calls.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        private void HandleFailure(Exception ex)
        {
            this.SwitchOnline(false);

            string message = ex.Message;
            if (ex.InnerException != null &&
                ex.InnerException is FaultException)
            {
                message = ex.InnerException.Message;
            }

            MessageBox.Show(
                "Format.Service failure: " + message,
                "Service failure",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            this.SwitchOnline(false);
        }

        /// <summary>
        /// Switches the "online" indicator on the UI.
        /// </summary>
        /// <param name="isOnline">
        /// Indicates whether the service is online or not.
        /// </param>
        /// <param name="version">The service's version.</param>
        public void SwitchOnline(bool isOnline, string version = "N/A")
        {
            try
            {
                // set on-/offline label
                toolStripLabelOnline.Visible = isOnline;
                this.toolStripLabelOffline.Visible = !this.toolStripLabelOnline.Visible;

                // connect/disconnect button
                this.toolStripButtonDeconnect.Visible = isOnline;
                this.toolStripButtonConnect.Visible = !this.toolStripButtonDeconnect.Visible;

                this.toolStripLabelServer.Enabled = !isOnline;
                this.toolStripTextBoxServer.Enabled = !isOnline;
                this.toolStripLabelCertificat.Enabled = !isOnline;
                this.toolStripTextBoxCertificat.Enabled = !isOnline;

                //// this.usernameLabel.Enabled = !isOnline;
                //// this.usernameTextBox.Enabled = !isOnline;
                //// this.passwordLabel.Enabled = !isOnline;
                //// this.passwordTextBox.Enabled = !isOnline;

                // main controls
                this.cBxFormats.Enabled = isOnline;
                this.gBxGetData.Enabled = isOnline;
            }
            catch (NullReferenceException)
            {
                // do nothing
                // this exception is only thrown if the windows closed while
                // the program is trying to connect.
            }
        }

        /// <summary>
        /// Switches the "waiting" state of the UI.
        /// </summary>
        /// <param name="isWaiting">
        /// Indicates whether the UI shall be set in "wait" mode or not.
        /// </param>
        private void SwitchWaiting(bool isWaiting)
        {
            this.Enabled = !isWaiting;

            Cursor.Current = isWaiting ? Cursors.WaitCursor : Cursors.Default;
        }

        private void SetProcessing(bool isProcessing)
        {
            gBxOutput.Enabled = !isProcessing;
            gBxGetData.Enabled = !isProcessing;
        }

        private void btnGetFormat_Click(object sender, EventArgs e)
        {
            SetProcessing(true);

        }

        private async void btnGetData_Click(object sender, EventArgs e)
        {
            switch (cBxFormats.SelectedIndex)
            {
                case 0:
                    format = "42ff4ea439524aa28ea0e61364c2704b";
                    MessageBox.Show("premier format");
                    break;
                case 1:
                    format = "a9cb28e0 - 70ed - 4b64 - b9bc - 200f45d65eb6";
                    break;
                case 2:
                    format = "af7d107b - 4e05 - 49b3 - bb06 - d0817356bbc7";
                    break;
                case 3:
                    format = "fcda2570 - 60db - 40fe - a7a3 - fd3b0b9f5b1d";
                    break;
                case 4:
                    format = "acfc6fd2 - 37e3 - 41a2 - 81be - c69ffa8be1cf";
                    break;
                default:
                    break;
            }


            this.SetProcessing(true);
            formatting = new FormattingServiceWrapper();
            try
            {
                // try to parse UID
                ulong uid;
                if (!ulong.TryParse(this.txBxID.Text, out uid))
                {
                    this.tBxResult.Text = "Invalid UID value supplied.";
                    return;
                }

                // try to parse FormatTypeId
                Guid formatTypeId;
                if (!Guid.TryParse(format, out formatTypeId))
                {
                    this.tBxResult.Text = "Invalid UUID value supplied for format type ID.";
                    return;
                }

                // synchronous call: var formattedData = this.formatting.Service.GetSingleUidData();
                var formattedData = await formatting.GetSingleUidDataAsync(uid, formatTypeId);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Formatted data:");
                sb.AppendLine();

                sb.Append("UID (dec): ");
                sb.AppendLine(formattedData.Uid.ToString());

                if (formattedData.Dsf.HasValue)
                {
                    sb.Append("DSF (dec): ");
                    sb.AppendLine(formattedData.Dsf.Value.ToString());
                }

                sb.AppendLine();
                string[] BlockDataToWrite = new string[formattedData.Blocks.Length];
                foreach (var blockData in formattedData.Blocks)
                {
                    BlockDataToWrite[blockData.BlockNumber] = blockData.BlockValue;
                    sb.Append("Block #");
                    sb.Append(blockData.BlockNumber);
                    sb.Append(": ");
                    sb.AppendLine(blockData.BlockValue);
                }

                this.tBxResult.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                this.tBxResult.Text =
                    string.Format(
                        "Error:{1}{1}{0}",
                        ex.Message,
                        Environment.NewLine);
            }

            this.SetProcessing(false);
        }
    }
}
