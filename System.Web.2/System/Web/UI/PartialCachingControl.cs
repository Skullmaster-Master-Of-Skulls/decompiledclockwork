using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002EC RID: 748
	public class PartialCachingControl : BasePartialCachingControl
	{
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000719E9 File Offset: 0x0006FBE9
		public Control CachedControl
		{
			get
			{
				return this._cachedCtrl;
			}
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000719F4 File Offset: 0x0006FBF4
		internal PartialCachingControl(IWebObjectFactory objectFactory, Type createCachedControlType, PartialCachingAttribute cacheAttrib, string cacheKey, object[] args)
		{
			string text = cacheAttrib.ProviderName;
			this._ctrlID = cacheKey;
			base.Duration = new TimeSpan(0, 0, cacheAttrib.Duration);
			base.SetVaryByParamsCollectionFromString(cacheAttrib.VaryByParams);
			if (cacheAttrib.VaryByControls != null)
			{
				this._varyByControlsCollection = cacheAttrib.VaryByControls.Split(new char[]
				{
					';'
				});
			}
			this._varyByCustom = cacheAttrib.VaryByCustom;
			this._sqlDependency = cacheAttrib.SqlDependency;
			if (text == "AspNetInternalProvider")
			{
				text = null;
			}
			this._provider = text;
			this._guid = cacheKey;
			this._objectFactory = objectFactory;
			this._createCachedControlType = createCachedControlType;
			this._args = args;
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00071AA8 File Offset: 0x0006FCA8
		internal override Control CreateCachedControl()
		{
			Control control;
			if (this._objectFactory != null)
			{
				control = (Control)this._objectFactory.CreateInstance();
			}
			else
			{
				control = (Control)HttpRuntime.CreatePublicInstance(this._createCachedControlType, this._args);
			}
			UserControl userControl = control as UserControl;
			if (userControl != null)
			{
				userControl.InitializeAsUserControl(this.Page);
			}
			control.ID = this._ctrlID;
			return control;
		}

		// Token: 0x04001C71 RID: 7281
		private IWebObjectFactory _objectFactory;

		// Token: 0x04001C72 RID: 7282
		private Type _createCachedControlType;

		// Token: 0x04001C73 RID: 7283
		private object[] _args;
	}
}
