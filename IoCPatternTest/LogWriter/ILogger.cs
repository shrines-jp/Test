using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogWriter
{
	public interface ILogger
	{
		public void WriteLog(string message);
	}
}
