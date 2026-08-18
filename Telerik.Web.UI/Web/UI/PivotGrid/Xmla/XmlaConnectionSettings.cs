using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D98 RID: 3480
	public class XmlaConnectionSettings
	{
		// Token: 0x06008168 RID: 33128 RVA: 0x001D868C File Offset: 0x001D688C
		public XmlaConnectionSettings()
		{
			this.queryProperties = new Collection<XmlaQueryProperty>();
		}

		// Token: 0x170028FF RID: 10495
		// (get) Token: 0x06008169 RID: 33129 RVA: 0x001D869F File Offset: 0x001D689F
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x17002900 RID: 10496
		// (get) Token: 0x0600816A RID: 33130 RVA: 0x001D86A6 File Offset: 0x001D68A6
		public Collection<XmlaQueryProperty> QueryProperties
		{
			get
			{
				return this.queryProperties;
			}
		}

		// Token: 0x17002901 RID: 10497
		// (get) Token: 0x0600816B RID: 33131 RVA: 0x001D86AE File Offset: 0x001D68AE
		// (set) Token: 0x0600816C RID: 33132 RVA: 0x001D86B6 File Offset: 0x001D68B6
		public string Cube { get; set; }

		// Token: 0x17002902 RID: 10498
		// (get) Token: 0x0600816D RID: 33133 RVA: 0x001D86BF File Offset: 0x001D68BF
		// (set) Token: 0x0600816E RID: 33134 RVA: 0x001D86C7 File Offset: 0x001D68C7
		public string Database { get; set; }

		// Token: 0x17002903 RID: 10499
		// (get) Token: 0x0600816F RID: 33135 RVA: 0x001D86D0 File Offset: 0x001D68D0
		// (set) Token: 0x06008170 RID: 33136 RVA: 0x001D86D8 File Offset: 0x001D68D8
		public string ServerAddress { get; set; }

		// Token: 0x17002904 RID: 10500
		// (get) Token: 0x06008171 RID: 33137 RVA: 0x001D86E1 File Offset: 0x001D68E1
		// (set) Token: 0x06008172 RID: 33138 RVA: 0x001D86E9 File Offset: 0x001D68E9
		public XmlaNetworkCredential Credentials { get; set; }

		// Token: 0x17002905 RID: 10501
		// (get) Token: 0x06008173 RID: 33139 RVA: 0x001D86F2 File Offset: 0x001D68F2
		// (set) Token: 0x06008174 RID: 33140 RVA: 0x001D86FA File Offset: 0x001D68FA
		public Encoding Encoding { get; set; }

		// Token: 0x06008175 RID: 33141 RVA: 0x001D8704 File Offset: 0x001D6904
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Database: {0}; Cube: {1}; Server address: {2}", new object[]
			{
				this.Database,
				this.Cube,
				this.ServerAddress
			});
		}

		// Token: 0x06008176 RID: 33142 RVA: 0x001D8744 File Offset: 0x001D6944
		public override bool Equals(object obj)
		{
			XmlaConnectionSettings xmlaConnectionSettings = obj as XmlaConnectionSettings;
			if (xmlaConnectionSettings != null)
			{
				return this.Cube == xmlaConnectionSettings.Cube && this.Database == xmlaConnectionSettings.Database && this.ServerAddress == xmlaConnectionSettings.ServerAddress;
			}
			return base.Equals(obj);
		}

		// Token: 0x06008177 RID: 33143 RVA: 0x001D87A2 File Offset: 0x001D69A2
		public override int GetHashCode()
		{
			return this.Cube.GetHashCode() ^ this.Database.GetHashCode() ^ this.ServerAddress.GetHashCode();
		}

		// Token: 0x06008178 RID: 33144 RVA: 0x001D87C7 File Offset: 0x001D69C7
		public static bool operator ==(XmlaConnectionSettings left, XmlaConnectionSettings right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06008179 RID: 33145 RVA: 0x001D87DD File Offset: 0x001D69DD
		public static bool operator !=(XmlaConnectionSettings left, XmlaConnectionSettings right)
		{
			return !(left == right);
		}

		// Token: 0x040023B3 RID: 9139
		private Collection<XmlaQueryProperty> queryProperties;
	}
}
