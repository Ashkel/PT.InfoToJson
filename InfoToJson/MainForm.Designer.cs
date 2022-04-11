
namespace InfoToJson
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtServerPath = new System.Windows.Forms.TextBox();
			this.btnBrowseServer = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.ckbIndentJson = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server:";
			// 
			// txtServerPath
			// 
			this.txtServerPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.txtServerPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.txtServerPath.BackColor = System.Drawing.SystemColors.Window;
			this.txtServerPath.Location = new System.Drawing.Point(66, 17);
			this.txtServerPath.Name = "txtServerPath";
			this.txtServerPath.Size = new System.Drawing.Size(220, 20);
			this.txtServerPath.TabIndex = 1;
			this.txtServerPath.Text = "C:\\Server";
			this.txtServerPath.TextChanged += new System.EventHandler(this.txtServerPath_TextChanged);
			// 
			// btnBrowseServer
			// 
			this.btnBrowseServer.Location = new System.Drawing.Point(291, 17);
			this.btnBrowseServer.Name = "btnBrowseServer";
			this.btnBrowseServer.Size = new System.Drawing.Size(21, 20);
			this.btnBrowseServer.TabIndex = 4;
			this.btnBrowseServer.Text = "...";
			this.btnBrowseServer.UseVisualStyleBackColor = true;
			this.btnBrowseServer.Click += new System.EventHandler(this.btnBrowseServer_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(87, 72);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(165, 35);
			this.btnStart.TabIndex = 6;
			this.btnStart.Text = "START";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Json:";
			// 
			// ckbIndentJson
			// 
			this.ckbIndentJson.AutoSize = true;
			this.ckbIndentJson.Location = new System.Drawing.Point(66, 47);
			this.ckbIndentJson.Name = "ckbIndentJson";
			this.ckbIndentJson.Size = new System.Drawing.Size(56, 17);
			this.ckbIndentJson.TabIndex = 8;
			this.ckbIndentJson.Text = "Indent";
			this.ckbIndentJson.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 116);
			this.Controls.Add(this.ckbIndentJson);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnBrowseServer);
			this.Controls.Add(this.txtServerPath);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtServerPath;
		private System.Windows.Forms.Button btnBrowseServer;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox ckbIndentJson;
	}
}

