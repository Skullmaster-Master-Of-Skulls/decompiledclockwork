using System;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000085 RID: 133
	internal interface IFileAppenderFactory
	{
		// Token: 0x0600046A RID: 1130
		BaseFileAppender Open(string fileName, ICreateFileParameters parameters);
	}
}
