using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using NLog.Targets;

namespace NLogSample
{
	public partial class Form1 : Form
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		//LogginngConfiguration 

		public Form1()
		{
			InitializeComponent();

			//FileTarget fileTarget = LogManager.Configuration.AllTargets.Where(t => t.Name == "txtFile").First() as FileTarget;

			//DateTime Now = DateTime.Now;

			//fileTarget.FileName = string.Format("{0}.txt", Now.ToString("yyyy-MM-dd HH-mm-ss"));
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//Logger logger = LogManager.GetLogger("NLogSample");
			logger.Log(LogLevel.Info, "Info Log Message = {0}, {1}", sender.ToString(), e.ToString());
			logger.Log(LogLevel.Trace, "Trace Log Message = {0}, {1}", sender.ToString(), e.ToString());
			logger.Log(LogLevel.Warn, "Warn Log Message = {0}, {1}", sender.ToString(), e.ToString());
			logger.Log(LogLevel.Debug, "Debug Log Message = {0}, {1}", sender.ToString(), e.ToString());
			logger.Log(LogLevel.Error, "Error Log Message = {0}, {1}", sender.ToString(), e.ToString());
			logger.Log(LogLevel.Fatal, "Fatal Log Message = {0}, {1}", sender.ToString(), e.ToString());
		}
	}
}
