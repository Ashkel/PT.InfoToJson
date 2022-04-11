using InfoToJson.Helper;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace InfoToJson
{
	public partial class MainForm : Form
	{
		public string ServerPath
		{
			get => this.txtServerPath.Text;
			set => this.txtServerPath.Text = value;
		}

		private Thread _thread = null;

		public MainForm()
		{
			InitializeComponent();
		}

		private void txtServerPath_TextChanged(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(ServerPath) || !Directory.Exists(ServerPath))
				txtServerPath.BackColor = Color.LightCoral;
			else
				txtServerPath.BackColor = SystemColors.Window;
		}

		private void btnBrowseServer_Click(object sender, EventArgs e)
		{
			try
			{
				var fbd = new FolderBrowserDialog();

				if(fbd.ShowDialog() == DialogResult.OK)				
					ServerPath = fbd.SelectedPath;
			}
			catch(Exception ex)
			{
				MessageBox.Show(Utils.FormatErrorMessage(ex, "btnBrowseServer_Click"), "Error!");
			}
		}

		private void btnStart_Click(object sender, EventArgs e)
		{

			if(_thread != null && _thread.IsAlive)
				return;

			_thread = new Thread(new InfoSerializer(ServerPath, ckbIndentJson.Checked).Go);

			_thread.Start();
		}
	}
}
