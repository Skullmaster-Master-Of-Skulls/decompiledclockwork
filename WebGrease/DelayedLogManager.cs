using System;
using System.Collections.Generic;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x02000034 RID: 52
	internal class DelayedLogManager
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0000866C File Offset: 0x0000686C
		public DelayedLogManager(LogManager syncLogManager, string messagePrefix = null)
		{
			DelayedLogManager <>4__this = this;
			this.messagePrefix = messagePrefix;
			this.LogManager = new LogManager(delegate(string m, MessageImportance importance)
			{
				<>4__this.AddTimedAction(m, delegate(string message)
				{
					syncLogManager.Information(message, importance);
				});
			}, delegate(string m)
			{
				<>4__this.AddTimedAction(m, new Action<string>(syncLogManager.Warning));
			}, delegate(string subcategory, string code, string keyword, string file, int? number, int? columnNumber, int? lineNumber, int? endColumnNumber, string m)
			{
				<>4__this.AddTimedAction(m, delegate(string message)
				{
					syncLogManager.Warning(subcategory, code, keyword, file, number, columnNumber, lineNumber, endColumnNumber, message);
				});
			}, delegate(string m)
			{
				<>4__this.AddTimedAction(m, new Action<string>(syncLogManager.Error));
			}, delegate(Exception exception, string m, string name)
			{
				<>4__this.AddTimedAction(m, delegate(string message)
				{
					syncLogManager.Error(exception, message, name);
				});
			}, delegate(string subcategory, string code, string keyword, string file, int? number, int? columnNumber, int? lineNumber, int? endColumnNumber, string m)
			{
				<>4__this.AddTimedAction(m, delegate(string message)
				{
					syncLogManager.Error(subcategory, code, keyword, file, number, columnNumber, lineNumber, endColumnNumber, message);
				});
			}, new bool?(false));
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00008744 File Offset: 0x00006944
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000874C File Offset: 0x0000694C
		public LogManager LogManager { get; private set; }

		// Token: 0x06000383 RID: 899 RVA: 0x000087B8 File Offset: 0x000069B8
		public void Flush()
		{
			if (!this.isFlushed)
			{
				Safe.Lock(this.flushLock, delegate()
				{
					if (!this.isFlushed)
					{
						this.isFlushed = true;
						this.actions.ForEach(delegate(Tuple<string, Action<string>> a)
						{
							a.Item2(a.Item1);
						});
						this.actions.Clear();
					}
				});
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00008844 File Offset: 0x00006A44
		private void AddTimedAction(string message, Action<string> action)
		{
			string formattedMessage = "{0} {1:HH:mm:ss.ff} {2}".InvariantFormat(new object[]
			{
				this.messagePrefix,
				DateTime.Now,
				message
			});
			Safe.Lock(this.flushLock, delegate()
			{
				if (this.isFlushed)
				{
					action(formattedMessage);
					return;
				}
				this.actions.Add(Tuple.Create<string, Action<string>>(formattedMessage, action));
			});
		}

		// Token: 0x040000A3 RID: 163
		private const string MessageFormat = "{0} {1:HH:mm:ss.ff} {2}";

		// Token: 0x040000A4 RID: 164
		private readonly string messagePrefix;

		// Token: 0x040000A5 RID: 165
		private readonly IList<Tuple<string, Action<string>>> actions = new List<Tuple<string, Action<string>>>();

		// Token: 0x040000A6 RID: 166
		private readonly object flushLock = new object();

		// Token: 0x040000A7 RID: 167
		private bool isFlushed;
	}
}
