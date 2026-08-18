using System;

namespace System.Web.UI
{
	// Token: 0x0200006D RID: 109
	public class ScriptBehaviorDescriptor : ScriptComponentDescriptor
	{
		// Token: 0x060003CB RID: 971 RVA: 0x00013F7D File Offset: 0x0001217D
		public ScriptBehaviorDescriptor(string type, string elementID) : base(type, elementID)
		{
			base.RegisterDispose = false;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00013F8E File Offset: 0x0001218E
		public override string ClientID
		{
			get
			{
				if (string.IsNullOrEmpty(this.ID))
				{
					return this.ElementID + "$" + this.Name;
				}
				return this.ID;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00013FBA File Offset: 0x000121BA
		public string ElementID
		{
			get
			{
				return base.ElementIDInternal;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00013FC2 File Offset: 0x000121C2
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00013FE3 File Offset: 0x000121E3
		public string Name
		{
			get
			{
				if (string.IsNullOrEmpty(this._name))
				{
					return ScriptBehaviorDescriptor.GetTypeName(base.Type);
				}
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00013FEC File Offset: 0x000121EC
		private static string GetTypeName(string type)
		{
			int num = type.LastIndexOf('.');
			if (num == -1)
			{
				return type;
			}
			return type.Substring(num + 1);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00014011 File Offset: 0x00012211
		protected internal override string GetScript()
		{
			if (!string.IsNullOrEmpty(this._name))
			{
				base.AddProperty("name", this._name);
			}
			return base.GetScript();
		}

		// Token: 0x04000174 RID: 372
		private string _name;
	}
}
