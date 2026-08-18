using System;

namespace System.Web.UI
{
	// Token: 0x020002EB RID: 747
	public class StaticPartialCachingControl : BasePartialCachingControl
	{
		// Token: 0x060022C8 RID: 8904 RVA: 0x000718B8 File Offset: 0x0006FAB8
		public StaticPartialCachingControl(string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, BuildMethod buildMethod) : this(ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, null, buildMethod, null)
		{
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000718D8 File Offset: 0x0006FAD8
		public StaticPartialCachingControl(string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, BuildMethod buildMethod) : this(ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, sqlDependency, buildMethod, null)
		{
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000718FC File Offset: 0x0006FAFC
		public StaticPartialCachingControl(string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, BuildMethod buildMethod, string providerName)
		{
			this._ctrlID = ctrlID;
			base.Duration = new TimeSpan(0, 0, duration);
			base.SetVaryByParamsCollectionFromString(varyByParams);
			if (varyByControls != null)
			{
				this._varyByControlsCollection = varyByControls.Split(new char[]
				{
					';'
				});
			}
			this._varyByCustom = varyByCustom;
			this._guid = guid;
			this._buildMethod = buildMethod;
			this._sqlDependency = sqlDependency;
			this._provider = providerName;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0007196F File Offset: 0x0006FB6F
		internal override Control CreateCachedControl()
		{
			return this._buildMethod();
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x0007197C File Offset: 0x0006FB7C
		public static void BuildCachedControl(Control parent, string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, BuildMethod buildMethod)
		{
			StaticPartialCachingControl.BuildCachedControl(parent, ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, null, buildMethod, null);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007199C File Offset: 0x0006FB9C
		public static void BuildCachedControl(Control parent, string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, BuildMethod buildMethod)
		{
			StaticPartialCachingControl.BuildCachedControl(parent, ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, sqlDependency, buildMethod, null);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000719C0 File Offset: 0x0006FBC0
		public static void BuildCachedControl(Control parent, string ctrlID, string guid, int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, BuildMethod buildMethod, string providerName)
		{
			StaticPartialCachingControl obj = new StaticPartialCachingControl(ctrlID, guid, duration, varyByParams, varyByControls, varyByCustom, sqlDependency, buildMethod, providerName);
			((IParserAccessor)parent).AddParsedSubObject(obj);
		}

		// Token: 0x04001C70 RID: 7280
		private BuildMethod _buildMethod;
	}
}
