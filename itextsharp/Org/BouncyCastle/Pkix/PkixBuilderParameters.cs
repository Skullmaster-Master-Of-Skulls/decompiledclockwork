using System;
using System.Text;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000233 RID: 563
	public class PkixBuilderParameters : PkixParameters
	{
		// Token: 0x060015F6 RID: 5622 RVA: 0x000809CC File Offset: 0x0007F9CC
		public static PkixBuilderParameters GetInstance(PkixParameters pkixParams)
		{
			PkixBuilderParameters pkixBuilderParameters = new PkixBuilderParameters(pkixParams.GetTrustAnchors(), new X509CertStoreSelector(pkixParams.GetTargetCertConstraints()));
			pkixBuilderParameters.SetParams(pkixParams);
			return pkixBuilderParameters;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000809F8 File Offset: 0x0007F9F8
		public PkixBuilderParameters(ISet trustAnchors, IX509Selector targetConstraints) : base(trustAnchors)
		{
			this.SetTargetCertConstraints(targetConstraints);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00080A1A File Offset: 0x0007FA1A
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x00080A22 File Offset: 0x0007FA22
		public virtual int MaxPathLength
		{
			get
			{
				return this.maxPathLength;
			}
			set
			{
				if (value < -1)
				{
					throw new InvalidParameterException("The maximum path length parameter can not be less than -1.");
				}
				this.maxPathLength = value;
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00080A3A File Offset: 0x0007FA3A
		public virtual ISet GetExcludedCerts()
		{
			return new HashSet(this.excludedCerts);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00080A47 File Offset: 0x0007FA47
		public virtual void SetExcludedCerts(ISet excludedCerts)
		{
			if (excludedCerts == null)
			{
				excludedCerts = new HashSet();
				return;
			}
			this.excludedCerts = new HashSet(excludedCerts);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00080A60 File Offset: 0x0007FA60
		protected override void SetParams(PkixParameters parameters)
		{
			base.SetParams(parameters);
			if (parameters is PkixBuilderParameters)
			{
				PkixBuilderParameters pkixBuilderParameters = (PkixBuilderParameters)parameters;
				this.maxPathLength = pkixBuilderParameters.maxPathLength;
				this.excludedCerts = new HashSet(pkixBuilderParameters.excludedCerts);
			}
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00080AA0 File Offset: 0x0007FAA0
		public override object Clone()
		{
			PkixBuilderParameters pkixBuilderParameters = new PkixBuilderParameters(this.GetTrustAnchors(), this.GetTargetCertConstraints());
			pkixBuilderParameters.SetParams(this);
			return pkixBuilderParameters;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00080AC8 File Offset: 0x0007FAC8
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PkixBuilderParameters [" + newLine);
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("  Maximum Path Length: ");
			stringBuilder.Append(this.MaxPathLength);
			stringBuilder.Append(newLine + "]" + newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x04000F3B RID: 3899
		private int maxPathLength = 5;

		// Token: 0x04000F3C RID: 3900
		private ISet excludedCerts = new HashSet();
	}
}
