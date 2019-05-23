using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogWriter
{
	/// <summary>
	/// 어떤 로그라도 사용할 수 있는 커스텀 Writer
	/// </summary>
	public sealed class Writer
	{
		/// <summary>
		/// 주입된 로거의 접근자
		/// </summary>
		private ILogger logger;


		/// <summary>
		/// Writer 클래스의 인스턴스 초기화
		/// </summary>
		/// <param name="logger"></param>
		public Writer(ILogger logger)
		{
			this.logger = logger;
		}

		/// <summary>
		/// 로그기록
		/// </summary>
		/// <param name="message"></param>
		public void Write(String message)
		{
			this.logger.WriteLog(message);
		}
	}
}
