using System;
using WebGrease.Activities;

namespace WebGrease
{
	// Token: 0x02000105 RID: 261
	public class LogManager
	{
		// Token: 0x06001089 RID: 4233 RVA: 0x00049D94 File Offset: 0x00047F94
		public LogManager(Action<string, MessageImportance> logInformation, Action<string> logWarning, LogExtendedError logExtendedWarning, Action<string> logErrorMessage, LogError logError, LogExtendedError logExtendedError, bool? treatWarningsAsErrors = false)
		{
			this.TreatWarningsAsErrors = true;
			if (treatWarningsAsErrors != null)
			{
				this.TreatWarningsAsErrors = (treatWarningsAsErrors == true);
			}
			this.information = logInformation;
			this.warning = logWarning;
			this.extendedWarning = logExtendedWarning;
			this.error = logError;
			this.errorMessage = logErrorMessage;
			this.extendedError = logExtendedError;
			this.HasExtendedErrorHandler = (logExtendedError != null);
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600108A RID: 4234 RVA: 0x00049E10 File Offset: 0x00048010
		// (remove) Token: 0x0600108B RID: 4235 RVA: 0x00049E48 File Offset: 0x00048048
		public event EventHandler ErrorOccurred;

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00049E7D File Offset: 0x0004807D
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x00049E85 File Offset: 0x00048085
		public bool TreatWarningsAsErrors { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x00049E8E File Offset: 0x0004808E
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x00049E96 File Offset: 0x00048096
		public bool HasExtendedErrorHandler { get; set; }

		// Token: 0x06001090 RID: 4240 RVA: 0x00049E9F File Offset: 0x0004809F
		public void Information(string message, MessageImportance messageImportance = MessageImportance.Normal)
		{
			if (this.information != null)
			{
				this.information(message, messageImportance);
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00049EB8 File Offset: 0x000480B8
		public void Warning(string message)
		{
			if (this.TreatWarningsAsErrors)
			{
				this.Error(message);
				return;
			}
			if (this.warning != null)
			{
				lock (LogManager.MessageLockObject)
				{
					this.warning(message);
				}
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00049F18 File Offset: 0x00048118
		public void Warning(string subcategory, string errorCode, string helpKeyword, string file, int? lineNumber, int? columnNumber, int? endLineNumber, int? endColumnNumber, string message)
		{
			if (this.TreatWarningsAsErrors)
			{
				this.Error(subcategory, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message);
				return;
			}
			if (this.extendedWarning != null)
			{
				lock (LogManager.MessageLockObject)
				{
					this.extendedWarning(subcategory, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message);
				}
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00049F94 File Offset: 0x00048194
		public void Error(string message)
		{
			this.ErrorHasOccurred();
			if (this.errorMessage != null)
			{
				lock (LogManager.MessageLockObject)
				{
					this.errorMessage(message);
				}
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00049FE8 File Offset: 0x000481E8
		public void Error(Exception exception, string customMessage = null, string file = null)
		{
			this.ErrorHasOccurred();
			BuildWorkflowException ex = exception as BuildWorkflowException;
			if (ex != null && this.extendedError != null)
			{
				lock (LogManager.MessageLockObject)
				{
					this.extendedError(ex.Subcategory, ex.ErrorCode, ex.HelpKeyword, ex.File, new int?(ex.LineNumber), new int?(ex.ColumnNumber), new int?(ex.EndLineNumber), new int?(ex.EndColumnNumber), ex.Message);
					return;
				}
			}
			if (this.error != null)
			{
				lock (LogManager.MessageLockObject)
				{
					this.error(exception, customMessage, file);
				}
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0004A0D0 File Offset: 0x000482D0
		public void Error(string subcategory, string errorCode, string helpKeyword, string file, int? lineNumber, int? columnNumber, int? endLineNumber, int? endColumnNumber, string message)
		{
			if (this.extendedError != null)
			{
				this.ErrorHasOccurred();
				lock (LogManager.MessageLockObject)
				{
					this.extendedError(subcategory, errorCode, helpKeyword, file, lineNumber, columnNumber, endLineNumber, endColumnNumber, message);
				}
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004A134 File Offset: 0x00048334
		private void ErrorHasOccurred()
		{
			if (this.ErrorOccurred != null)
			{
				lock (LogManager.MessageLockObject)
				{
					this.ErrorOccurred(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x0400066E RID: 1646
		private static readonly object MessageLockObject = new object();

		// Token: 0x0400066F RID: 1647
		private readonly Action<string, MessageImportance> information;

		// Token: 0x04000670 RID: 1648
		private readonly LogExtendedError extendedWarning;

		// Token: 0x04000671 RID: 1649
		private readonly Action<string> warning;

		// Token: 0x04000672 RID: 1650
		private readonly LogError error;

		// Token: 0x04000673 RID: 1651
		private readonly Action<string> errorMessage;

		// Token: 0x04000674 RID: 1652
		private readonly LogExtendedError extendedError;
	}
}
