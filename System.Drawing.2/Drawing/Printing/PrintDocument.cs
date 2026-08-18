using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Drawing.Printing
{
	// Token: 0x02000061 RID: 97
	[ToolboxItemFilter("System.Drawing.Printing")]
	[DefaultProperty("DocumentName")]
	[SRDescription("PrintDocumentDesc")]
	[DefaultEvent("PrintPage")]
	public class PrintDocument : Component
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0001E6BE File Offset: 0x0001C8BE
		public PrintDocument()
		{
			this.defaultPageSettings = new PageSettings(this.printerSettings);
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001E6ED File Offset: 0x0001C8ED
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0001E6F5 File Offset: 0x0001C8F5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PDOCdocumentPageSettingsDescr")]
		public PageSettings DefaultPageSettings
		{
			get
			{
				return this.defaultPageSettings;
			}
			set
			{
				if (value == null)
				{
					value = new PageSettings();
				}
				this.defaultPageSettings = value;
				this.userSetPageSettings = true;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001E70F File Offset: 0x0001C90F
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0001E717 File Offset: 0x0001C917
		[DefaultValue("document")]
		[SRDescription("PDOCdocumentNameDescr")]
		public string DocumentName
		{
			get
			{
				return this.documentName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				this.documentName = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001E72A File Offset: 0x0001C92A
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0001E732 File Offset: 0x0001C932
		[DefaultValue(false)]
		[SRDescription("PDOCoriginAtMarginsDescr")]
		public bool OriginAtMargins
		{
			get
			{
				return this.originAtMargins;
			}
			set
			{
				this.originAtMargins = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001E73C File Offset: 0x0001C93C
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0001E81C File Offset: 0x0001CA1C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PDOCprintControllerDescr")]
		public PrintController PrintController
		{
			get
			{
				IntSecurity.SafePrinting.Demand();
				if (this.printController == null)
				{
					this.printController = new StandardPrintController();
					new ReflectionPermission(PermissionState.Unrestricted).Assert();
					try
					{
						Type type = Type.GetType("System.Windows.Forms.PrintControllerWithStatusDialog, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
						this.printController = (PrintController)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[]
						{
							this.printController
						}, null);
					}
					catch (TypeLoadException)
					{
					}
					catch (TargetInvocationException)
					{
					}
					catch (MissingMethodException)
					{
					}
					catch (MethodAccessException)
					{
					}
					catch (MemberAccessException)
					{
					}
					catch (FileNotFoundException)
					{
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return this.printController;
			}
			set
			{
				IntSecurity.SafePrinting.Demand();
				this.printController = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001E82F File Offset: 0x0001CA2F
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0001E837 File Offset: 0x0001CA37
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PDOCprinterSettingsDescr")]
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printerSettings;
			}
			set
			{
				if (value == null)
				{
					value = new PrinterSettings();
				}
				this.printerSettings = value;
				if (!this.userSetPageSettings)
				{
					this.defaultPageSettings = this.printerSettings.DefaultPageSettings;
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000782 RID: 1922 RVA: 0x0001E863 File Offset: 0x0001CA63
		// (remove) Token: 0x06000783 RID: 1923 RVA: 0x0001E87C File Offset: 0x0001CA7C
		[SRDescription("PDOCbeginPrintDescr")]
		public event PrintEventHandler BeginPrint
		{
			add
			{
				this.beginPrintHandler = (PrintEventHandler)Delegate.Combine(this.beginPrintHandler, value);
			}
			remove
			{
				this.beginPrintHandler = (PrintEventHandler)Delegate.Remove(this.beginPrintHandler, value);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000784 RID: 1924 RVA: 0x0001E895 File Offset: 0x0001CA95
		// (remove) Token: 0x06000785 RID: 1925 RVA: 0x0001E8AE File Offset: 0x0001CAAE
		[SRDescription("PDOCendPrintDescr")]
		public event PrintEventHandler EndPrint
		{
			add
			{
				this.endPrintHandler = (PrintEventHandler)Delegate.Combine(this.endPrintHandler, value);
			}
			remove
			{
				this.endPrintHandler = (PrintEventHandler)Delegate.Remove(this.endPrintHandler, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000786 RID: 1926 RVA: 0x0001E8C7 File Offset: 0x0001CAC7
		// (remove) Token: 0x06000787 RID: 1927 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
		[SRDescription("PDOCprintPageDescr")]
		public event PrintPageEventHandler PrintPage
		{
			add
			{
				this.printPageHandler = (PrintPageEventHandler)Delegate.Combine(this.printPageHandler, value);
			}
			remove
			{
				this.printPageHandler = (PrintPageEventHandler)Delegate.Remove(this.printPageHandler, value);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000788 RID: 1928 RVA: 0x0001E8F9 File Offset: 0x0001CAF9
		// (remove) Token: 0x06000789 RID: 1929 RVA: 0x0001E912 File Offset: 0x0001CB12
		[SRDescription("PDOCqueryPageSettingsDescr")]
		public event QueryPageSettingsEventHandler QueryPageSettings
		{
			add
			{
				this.queryHandler = (QueryPageSettingsEventHandler)Delegate.Combine(this.queryHandler, value);
			}
			remove
			{
				this.queryHandler = (QueryPageSettingsEventHandler)Delegate.Remove(this.queryHandler, value);
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001E92B File Offset: 0x0001CB2B
		internal void _OnBeginPrint(PrintEventArgs e)
		{
			this.OnBeginPrint(e);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001E934 File Offset: 0x0001CB34
		protected virtual void OnBeginPrint(PrintEventArgs e)
		{
			if (this.beginPrintHandler != null)
			{
				this.beginPrintHandler(this, e);
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001E94B File Offset: 0x0001CB4B
		internal void _OnEndPrint(PrintEventArgs e)
		{
			this.OnEndPrint(e);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001E954 File Offset: 0x0001CB54
		protected virtual void OnEndPrint(PrintEventArgs e)
		{
			if (this.endPrintHandler != null)
			{
				this.endPrintHandler(this, e);
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001E96B File Offset: 0x0001CB6B
		internal void _OnPrintPage(PrintPageEventArgs e)
		{
			this.OnPrintPage(e);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001E974 File Offset: 0x0001CB74
		protected virtual void OnPrintPage(PrintPageEventArgs e)
		{
			if (this.printPageHandler != null)
			{
				this.printPageHandler(this, e);
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001E98B File Offset: 0x0001CB8B
		internal void _OnQueryPageSettings(QueryPageSettingsEventArgs e)
		{
			this.OnQueryPageSettings(e);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001E994 File Offset: 0x0001CB94
		protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e)
		{
			if (this.queryHandler != null)
			{
				this.queryHandler(this, e);
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001E9AC File Offset: 0x0001CBAC
		public void Print()
		{
			if (!this.PrinterSettings.IsDefaultPrinter && !this.PrinterSettings.PrintDialogDisplayed)
			{
				IntSecurity.AllPrinting.Demand();
			}
			PrintController printController = this.PrintController;
			printController.Print(this);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001E9EB File Offset: 0x0001CBEB
		public override string ToString()
		{
			return "[PrintDocument " + this.DocumentName + "]";
		}

		// Token: 0x040006C0 RID: 1728
		private string documentName = "document";

		// Token: 0x040006C1 RID: 1729
		private PrintEventHandler beginPrintHandler;

		// Token: 0x040006C2 RID: 1730
		private PrintEventHandler endPrintHandler;

		// Token: 0x040006C3 RID: 1731
		private PrintPageEventHandler printPageHandler;

		// Token: 0x040006C4 RID: 1732
		private QueryPageSettingsEventHandler queryHandler;

		// Token: 0x040006C5 RID: 1733
		private PrinterSettings printerSettings = new PrinterSettings();

		// Token: 0x040006C6 RID: 1734
		private PageSettings defaultPageSettings;

		// Token: 0x040006C7 RID: 1735
		private PrintController printController;

		// Token: 0x040006C8 RID: 1736
		private bool originAtMargins;

		// Token: 0x040006C9 RID: 1737
		private bool userSetPageSettings;
	}
}
