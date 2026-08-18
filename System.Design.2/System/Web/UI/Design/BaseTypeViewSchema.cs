using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal abstract class BaseTypeViewSchema : IDataSourceViewSchema
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000034E4 File Offset: 0x000016E4
		protected BaseTypeViewSchema(string viewName, Type type)
		{
			this._type = type;
			this._viewName = viewName;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000034FC File Offset: 0x000016FC
		public IDataSourceFieldSchema[] GetFields()
		{
			List<IDataSourceFieldSchema> list = new List<IDataSourceFieldSchema>();
			Type rowType = this.GetRowType(this._type);
			if (rowType != null && !typeof(ICustomTypeDescriptor).IsAssignableFrom(rowType))
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(rowType);
				foreach (object obj in properties)
				{
					PropertyDescriptor fieldDescriptor = (PropertyDescriptor)obj;
					list.Add(new TypeFieldSchema(fieldDescriptor));
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003598 File Offset: 0x00001798
		public IDataSourceViewSchema[] GetChildren()
		{
			return null;
		}

		// Token: 0x0600002B RID: 43
		protected abstract Type GetRowType(Type objectType);

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000359B File Offset: 0x0000179B
		public string Name
		{
			get
			{
				return this._viewName;
			}
		}

		// Token: 0x040000B6 RID: 182
		private Type _type;

		// Token: 0x040000B7 RID: 183
		private string _viewName;
	}
}
