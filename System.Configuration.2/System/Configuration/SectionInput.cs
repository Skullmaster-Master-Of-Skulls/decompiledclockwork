using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Configuration
{
	// Token: 0x02000088 RID: 136
	[DebuggerDisplay("SectionInput {_sectionXmlInfo.ConfigKey}")]
	internal class SectionInput
	{
		// Token: 0x0600055D RID: 1373 RVA: 0x0001B5B9 File Offset: 0x000197B9
		internal SectionInput(SectionXmlInfo sectionXmlInfo, List<ConfigurationException> errors)
		{
			this._sectionXmlInfo = sectionXmlInfo;
			this._errors = errors;
			this._result = SectionInput.s_unevaluated;
			this._resultRuntimeObject = SectionInput.s_unevaluated;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001B5E5 File Offset: 0x000197E5
		internal SectionXmlInfo SectionXmlInfo
		{
			get
			{
				return this._sectionXmlInfo;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001B5ED File Offset: 0x000197ED
		internal bool HasResult
		{
			get
			{
				return this._result != SectionInput.s_unevaluated;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001B5FF File Offset: 0x000197FF
		internal bool HasResultRuntimeObject
		{
			get
			{
				return this._resultRuntimeObject != SectionInput.s_unevaluated;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001B611 File Offset: 0x00019811
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001B619 File Offset: 0x00019819
		internal object Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001B622 File Offset: 0x00019822
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001B62A File Offset: 0x0001982A
		internal object ResultRuntimeObject
		{
			get
			{
				return this._resultRuntimeObject;
			}
			set
			{
				this._resultRuntimeObject = value;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001B633 File Offset: 0x00019833
		internal void ClearResult()
		{
			this._result = SectionInput.s_unevaluated;
			this._resultRuntimeObject = SectionInput.s_unevaluated;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001B64B File Offset: 0x0001984B
		internal bool IsConfigBuilderDetermined
		{
			get
			{
				return this._isConfigBuilderDetermined;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001B653 File Offset: 0x00019853
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001B65B File Offset: 0x0001985B
		internal ConfigurationBuilder ConfigBuilder
		{
			get
			{
				return this._configBuilder;
			}
			set
			{
				this._configBuilder = value;
				this._isConfigBuilderDetermined = true;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001B66B File Offset: 0x0001986B
		internal bool IsProtectionProviderDetermined
		{
			get
			{
				return this._isProtectionProviderDetermined;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0001B673 File Offset: 0x00019873
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0001B67B File Offset: 0x0001987B
		internal ProtectedConfigurationProvider ProtectionProvider
		{
			get
			{
				return this._protectionProvider;
			}
			set
			{
				this._protectionProvider = value;
				this._isProtectionProviderDetermined = true;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001B68B File Offset: 0x0001988B
		internal ICollection<ConfigurationException> Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001B693 File Offset: 0x00019893
		internal bool HasErrors
		{
			get
			{
				return ErrorsHelper.GetHasErrors(this._errors);
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001B6A0 File Offset: 0x000198A0
		internal void ThrowOnErrors()
		{
			ErrorsHelper.ThrowOnErrors(this._errors);
		}

		// Token: 0x04000314 RID: 788
		private static object s_unevaluated = new object();

		// Token: 0x04000315 RID: 789
		private SectionXmlInfo _sectionXmlInfo;

		// Token: 0x04000316 RID: 790
		private ConfigurationBuilder _configBuilder;

		// Token: 0x04000317 RID: 791
		private bool _isConfigBuilderDetermined;

		// Token: 0x04000318 RID: 792
		private ProtectedConfigurationProvider _protectionProvider;

		// Token: 0x04000319 RID: 793
		private bool _isProtectionProviderDetermined;

		// Token: 0x0400031A RID: 794
		private object _result;

		// Token: 0x0400031B RID: 795
		private object _resultRuntimeObject;

		// Token: 0x0400031C RID: 796
		private List<ConfigurationException> _errors;
	}
}
