using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Data;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000096 RID: 150
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class AccessDataSourceDesigner : SqlDataSourceDesigner
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001455E File Offset: 0x0001275E
		private AccessDataSource AccessDataSource
		{
			get
			{
				return (AccessDataSource)base.Component;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001456B File Offset: 0x0001276B
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00014578 File Offset: 0x00012778
		public string DataFile
		{
			get
			{
				return this.AccessDataSource.DataFile;
			}
			set
			{
				if (value != this.DataFile)
				{
					this.AccessDataSource.DataFile = value;
					this.UpdateDesignTimeHtml();
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000145A5 File Offset: 0x000127A5
		internal override SqlDataSourceWizardForm CreateConfigureDataSourceWizardForm(IServiceProvider serviceProvider, IDataEnvironment dataEnvironment)
		{
			return new AccessDataSourceWizardForm(serviceProvider, this, dataEnvironment);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000145AF File Offset: 0x000127AF
		protected override string GetConnectionString()
		{
			return AccessDataSourceDesigner.GetConnectionString(base.Component.Site, this.AccessDataSource);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000145C8 File Offset: 0x000127C8
		internal static string GetConnectionString(IServiceProvider serviceProvider, AccessDataSource dataSource)
		{
			string dataFile = dataSource.DataFile;
			string connectionString;
			try
			{
				if (dataFile.Length == 0)
				{
					return null;
				}
				dataSource.DataFile = UrlPath.MapPath(serviceProvider, dataFile);
				connectionString = dataSource.ConnectionString;
			}
			finally
			{
				dataSource.DataFile = dataFile;
			}
			return connectionString;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001461C File Offset: 0x0001281C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor oldPropertyDescriptor = (PropertyDescriptor)properties["DataFile"];
			properties["DataFile"] = TypeDescriptor.CreateProperty(base.GetType(), oldPropertyDescriptor, new Attribute[0]);
		}
	}
}
