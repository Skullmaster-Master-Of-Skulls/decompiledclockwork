using System;
using System.Diagnostics;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x0200003A RID: 58
	[DebuggerDisplay("Name: {Name}, IsTopLevel: {IsTopLevel}")]
	public class ExceptionContextCatchBlock
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00007123 File Offset: 0x00005323
		public ExceptionContextCatchBlock(string name, bool isTopLevel, bool callsHandler)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._name = name;
			this._isTopLevel = isTopLevel;
			this._callsHandler = callsHandler;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000714E File Offset: 0x0000534E
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007156 File Offset: 0x00005356
		public bool IsTopLevel
		{
			get
			{
				return this._isTopLevel;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000715E File Offset: 0x0000535E
		public bool CallsHandler
		{
			get
			{
				return this._callsHandler;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007166 File Offset: 0x00005366
		public override string ToString()
		{
			return this._name;
		}

		// Token: 0x04000081 RID: 129
		private readonly string _name;

		// Token: 0x04000082 RID: 130
		private readonly bool _isTopLevel;

		// Token: 0x04000083 RID: 131
		private readonly bool _callsHandler;
	}
}
