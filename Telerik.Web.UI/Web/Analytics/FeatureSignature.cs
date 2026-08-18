using System;

namespace Telerik.Web.Analytics
{
	// Token: 0x0200047D RID: 1149
	public sealed class FeatureSignature : IFeatureSignature, IFeatureTraceHandler, IDisposable, IFeatureContract
	{
		// Token: 0x06002904 RID: 10500 RVA: 0x000844E5 File Offset: 0x000826E5
		public FeatureSignature()
		{
			this._isActive = true;
			this.FeatureClass = FeatureClass.Other;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x000844FB File Offset: 0x000826FB
		public IFeatureSignature Signature
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000844FE File Offset: 0x000826FE
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00084505 File Offset: 0x00082705
		void IFeatureTraceHandler.End()
		{
			this.EndFeature(false);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0008450E File Offset: 0x0008270E
		void IFeatureTraceHandler.Cancel()
		{
			this.EndFeature(true);
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x00084517 File Offset: 0x00082717
		void IFeatureTraceHandler.TraceValue(long value)
		{
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x00084519 File Offset: 0x00082719
		void IFeatureTraceHandler.TraceError(Exception exception)
		{
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0008451B File Offset: 0x0008271B
		private void EndFeature(bool isCancel)
		{
			if (this._isActive)
			{
				this._isActive = false;
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x0008452E File Offset: 0x0008272E
		public IFeatureContract OfControlType(Type type)
		{
			this.ControlType = type;
			return this;
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00084538 File Offset: 0x00082738
		public IFeatureContract OfGroup(string group)
		{
			this.FeatureGroup = group;
			return this;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00084542 File Offset: 0x00082742
		public IFeatureContract OfInstance(IFeatureGroup control)
		{
			this.FeatureGroup = control.FeatureGroupID;
			return this;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00084551 File Offset: 0x00082751
		public IFeatureContract OfClass(FeatureClass featureClass)
		{
			this.FeatureClass = featureClass;
			return this;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x0008455B File Offset: 0x0008275B
		public IFeatureContract OfPriority(FeaturePriority level)
		{
			this.FeaturePriority = level;
			return this;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00084565 File Offset: 0x00082765
		public IFeatureContract OfName(Func<string> result)
		{
			this.FeatureName = result();
			return this;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00084574 File Offset: 0x00082774
		public IFeatureContract OfValue(Func<string> result)
		{
			this.FeatureValue = result();
			return this;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00084583 File Offset: 0x00082783
		public IFeatureContract OfType(FeatureType type)
		{
			this.FeatureType = type;
			return this;
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x0008458D File Offset: 0x0008278D
		// (set) Token: 0x06002915 RID: 10517 RVA: 0x00084595 File Offset: 0x00082795
		public string FeatureName { get; set; }

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x0008459E File Offset: 0x0008279E
		// (set) Token: 0x06002917 RID: 10519 RVA: 0x000845A6 File Offset: 0x000827A6
		public string FeatureGroup { get; set; }

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x000845AF File Offset: 0x000827AF
		// (set) Token: 0x06002919 RID: 10521 RVA: 0x000845B7 File Offset: 0x000827B7
		public string FeatureValue { get; set; }

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x000845C0 File Offset: 0x000827C0
		// (set) Token: 0x0600291B RID: 10523 RVA: 0x000845C8 File Offset: 0x000827C8
		public FeatureClass FeatureClass { get; set; }

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x000845D1 File Offset: 0x000827D1
		// (set) Token: 0x0600291D RID: 10525 RVA: 0x000845D9 File Offset: 0x000827D9
		public FeaturePriority FeaturePriority { get; set; }

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000845E2 File Offset: 0x000827E2
		// (set) Token: 0x0600291F RID: 10527 RVA: 0x000845EA File Offset: 0x000827EA
		public FeatureType FeatureType { get; set; }

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000845F3 File Offset: 0x000827F3
		// (set) Token: 0x06002921 RID: 10529 RVA: 0x000845FB File Offset: 0x000827FB
		public Type ControlType { get; set; }

		// Token: 0x04000A6B RID: 2667
		private bool _isActive;
	}
}
