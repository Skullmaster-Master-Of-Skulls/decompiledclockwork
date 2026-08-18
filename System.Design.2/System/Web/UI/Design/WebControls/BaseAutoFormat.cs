using System;
using System.Data;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200009B RID: 155
	internal abstract class BaseAutoFormat<T> : DesignerAutoFormat where T : Control
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x000157B0 File Offset: 0x000139B0
		public BaseAutoFormat(string schemeName, string schemes) : base(SR.GetString(schemeName))
		{
			this._schemes = schemes;
			this._schemeName = schemeName;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000157CC File Offset: 0x000139CC
		public override void Apply(Control control)
		{
			T t = control as T;
			if (t != null)
			{
				this.EnsureInitialized();
				this.Apply(t);
			}
		}

		// Token: 0x0600049C RID: 1180
		protected abstract void Apply(T control);

		// Token: 0x0600049D RID: 1181 RVA: 0x000157FC File Offset: 0x000139FC
		private void EnsureInitialized()
		{
			if (!this._initialized)
			{
				DataRow schemeDataRow = ControlDesigner.GetSchemeDataRow(this._schemeName, this._schemes);
				this.Initialize(schemeDataRow);
				this._initialized = true;
			}
		}

		// Token: 0x0600049E RID: 1182
		protected abstract void Initialize(DataRow schemeData);

		// Token: 0x0600049F RID: 1183 RVA: 0x00015834 File Offset: 0x00013A34
		protected static bool GetBooleanProperty(string propertyTag, DataRow schemeData)
		{
			object obj = schemeData[propertyTag];
			return obj != null && !obj.Equals(DBNull.Value) && bool.Parse(obj.ToString());
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015866 File Offset: 0x00013A66
		protected static int GetIntProperty(string propertyTag, DataRow schemeData)
		{
			return BaseAutoFormat<T>.GetIntProperty(propertyTag, schemeData, 0);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00015870 File Offset: 0x00013A70
		protected static int GetIntProperty(string propertyTag, int defaultValue, DataRow schemeData)
		{
			return BaseAutoFormat<T>.GetIntProperty(propertyTag, schemeData, defaultValue);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001587C File Offset: 0x00013A7C
		protected static int GetIntProperty(string propertyTag, DataRow schemeData, int defaultValue)
		{
			object obj = schemeData[propertyTag];
			if (obj != null && !obj.Equals(DBNull.Value))
			{
				return int.Parse(obj.ToString(), CultureInfo.InvariantCulture);
			}
			return defaultValue;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000158B3 File Offset: 0x00013AB3
		protected static string GetStringProperty(string propertyTag, DataRow schemeData)
		{
			return BaseAutoFormat<T>.GetStringProperty(propertyTag, schemeData, string.Empty);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000158C4 File Offset: 0x00013AC4
		protected static string GetStringProperty(string propertyTag, DataRow schemeData, string defaultValue)
		{
			object obj = schemeData[propertyTag];
			if (obj != null && !obj.Equals(DBNull.Value))
			{
				return obj.ToString();
			}
			return defaultValue;
		}

		// Token: 0x0400020F RID: 527
		private readonly string _schemeName;

		// Token: 0x04000210 RID: 528
		private readonly string _schemes;

		// Token: 0x04000211 RID: 529
		private bool _initialized;
	}
}
