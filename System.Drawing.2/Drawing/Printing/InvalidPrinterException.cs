using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class InvalidPrinterException : SystemException
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		public InvalidPrinterException(PrinterSettings settings) : base(InvalidPrinterException.GenerateMessage(settings))
		{
			this.settings = settings;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001C9B9 File Offset: 0x0001ABB9
		protected InvalidPrinterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.settings = (PrinterSettings)info.GetValue("settings", typeof(PrinterSettings));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001C9E3 File Offset: 0x0001ABE3
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			IntSecurity.AllPrinting.Demand();
			info.AddValue("settings", this.settings);
			base.GetObjectData(info, context);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001CA18 File Offset: 0x0001AC18
		private static string GenerateMessage(PrinterSettings settings)
		{
			if (settings.IsDefaultPrinter)
			{
				return SR.GetString("InvalidPrinterException_NoDefaultPrinter");
			}
			string @string;
			try
			{
				@string = SR.GetString("InvalidPrinterException_InvalidPrinter", new object[]
				{
					settings.PrinterName
				});
			}
			catch (SecurityException)
			{
				@string = SR.GetString("InvalidPrinterException_InvalidPrinter", new object[]
				{
					SR.GetString("CantTellPrinterName")
				});
			}
			return @string;
		}

		// Token: 0x0400060C RID: 1548
		private PrinterSettings settings;
	}
}
