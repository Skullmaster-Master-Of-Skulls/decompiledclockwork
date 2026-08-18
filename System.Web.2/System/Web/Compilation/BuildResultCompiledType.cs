using System;
using System.Reflection;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000818 RID: 2072
	internal class BuildResultCompiledType : BuildResultCompiledAssemblyBase, ITypedWebObjectFactory, IWebObjectFactory
	{
		// Token: 0x06006341 RID: 25409 RVA: 0x0015BC5C File Offset: 0x00159E5C
		internal BuildResultCompiledType()
		{
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x0015BECD File Offset: 0x0015A0CD
		internal BuildResultCompiledType(Type t)
		{
			this._builtType = t;
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x00118C96 File Offset: 0x00116E96
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.BuildResultCompiledType;
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x06006344 RID: 25412 RVA: 0x0015BEDC File Offset: 0x0015A0DC
		// (set) Token: 0x06006345 RID: 25413 RVA: 0x00006164 File Offset: 0x00004364
		internal override Assembly ResultAssembly
		{
			get
			{
				return this._builtType.Assembly;
			}
			set
			{
			}
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x06006346 RID: 25414 RVA: 0x0015BEE9 File Offset: 0x0015A0E9
		internal override bool HasResultAssembly
		{
			get
			{
				return this._builtType != null;
			}
		}

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x06006347 RID: 25415 RVA: 0x0015BEF7 File Offset: 0x0015A0F7
		protected override bool IsGacAssembly
		{
			get
			{
				return !this.IsDelayLoadType && base.IsGacAssembly;
			}
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x06006348 RID: 25416 RVA: 0x0015BF0C File Offset: 0x0015A10C
		protected override string ShortAssemblyName
		{
			get
			{
				DelayLoadType delayLoadType = this.ResultType as DelayLoadType;
				if (delayLoadType != null)
				{
					return delayLoadType.AssemblyName;
				}
				return base.ShortAssemblyName;
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x06006349 RID: 25417 RVA: 0x0015BF3B File Offset: 0x0015A13B
		// (set) Token: 0x0600634A RID: 25418 RVA: 0x0015BF43 File Offset: 0x0015A143
		internal Type ResultType
		{
			get
			{
				return this._builtType;
			}
			set
			{
				this._builtType = value;
			}
		}

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x0600634B RID: 25419 RVA: 0x0015BF4C File Offset: 0x0015A14C
		private string FullResultTypeName
		{
			get
			{
				DelayLoadType delayLoadType = this.ResultType as DelayLoadType;
				if (delayLoadType != null)
				{
					return delayLoadType.TypeName;
				}
				return this.ResultType.FullName;
			}
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x0600634C RID: 25420 RVA: 0x0015BF80 File Offset: 0x0015A180
		internal bool IsDelayLoadType
		{
			get
			{
				return this.ResultType is DelayLoadType;
			}
		}

		// Token: 0x0600634D RID: 25421 RVA: 0x0015BF90 File Offset: 0x0015A190
		internal static bool UsesDelayLoadType(BuildResult result)
		{
			BuildResultCompiledType buildResultCompiledType = result as BuildResultCompiledType;
			return buildResultCompiledType != null && buildResultCompiledType.IsDelayLoadType;
		}

		// Token: 0x0600634E RID: 25422 RVA: 0x0015BFB0 File Offset: 0x0015A1B0
		public object CreateInstance()
		{
			if (!this._triedToGetInstObj)
			{
				this._instObj = ObjectFactoryCodeDomTreeGenerator.GetFastObjectCreationDelegate(this.ResultType);
				this._triedToGetInstObj = true;
			}
			if (this._instObj == null)
			{
				return HttpRuntime.CreatePublicInstanceByWebObjectActivator(this.ResultType);
			}
			return this._instObj();
		}

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x0600634F RID: 25423 RVA: 0x0015BFFC File Offset: 0x0015A1FC
		public virtual Type InstantiatedType
		{
			get
			{
				return this.ResultType;
			}
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x0015C004 File Offset: 0x0015A204
		protected override void ComputeHashCode(HashCodeCombiner hashCodeCombiner)
		{
			base.ComputeHashCode(hashCodeCombiner);
			if (base.VirtualPath != null)
			{
				VirtualPath parent = base.VirtualPath.Parent;
				Assembly localResourcesAssembly = BuildManager.GetLocalResourcesAssembly(parent);
				if (localResourcesAssembly != null)
				{
					hashCodeCombiner.AddFile(localResourcesAssembly.Location);
				}
			}
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x0015C050 File Offset: 0x0015A250
		internal override void GetPreservedAttributes(PreservationFileReader pfr)
		{
			base.GetPreservedAttributes(pfr);
			Assembly preservedAssembly = BuildResultCompiledAssemblyBase.GetPreservedAssembly(pfr);
			string attribute = pfr.GetAttribute("type");
			this.ResultType = preservedAssembly.GetType(attribute, true);
		}

		// Token: 0x06006352 RID: 25426 RVA: 0x0015C085 File Offset: 0x0015A285
		internal override void SetPreservedAttributes(PreservationFileWriter pfw)
		{
			base.SetPreservedAttributes(pfw);
			pfw.SetAttribute("type", this.FullResultTypeName);
		}

		// Token: 0x04003377 RID: 13175
		private InstantiateObject _instObj;

		// Token: 0x04003378 RID: 13176
		private bool _triedToGetInstObj;

		// Token: 0x04003379 RID: 13177
		private Type _builtType;
	}
}
