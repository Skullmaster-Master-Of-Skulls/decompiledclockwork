using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x020000FD RID: 253
	public class X509AttrCertStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x0003323B File Offset: 0x0003223B
		public X509AttrCertStoreSelector()
		{
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0003325C File Offset: 0x0003225C
		private X509AttrCertStoreSelector(X509AttrCertStoreSelector o)
		{
			this.attributeCert = o.attributeCert;
			this.attributeCertificateValid = o.attributeCertificateValid;
			this.holder = o.holder;
			this.issuer = o.issuer;
			this.serialNumber = o.serialNumber;
			this.targetGroups = new HashSet(o.targetGroups);
			this.targetNames = new HashSet(o.targetNames);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000332E4 File Offset: 0x000322E4
		public bool Match(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			IX509AttributeCertificate ix509AttributeCertificate = obj as IX509AttributeCertificate;
			if (ix509AttributeCertificate == null)
			{
				return false;
			}
			if (this.attributeCert != null && !this.attributeCert.Equals(ix509AttributeCertificate))
			{
				return false;
			}
			if (this.serialNumber != null && !ix509AttributeCertificate.SerialNumber.Equals(this.serialNumber))
			{
				return false;
			}
			if (this.holder != null && !ix509AttributeCertificate.Holder.Equals(this.holder))
			{
				return false;
			}
			if (this.issuer != null && !ix509AttributeCertificate.Issuer.Equals(this.issuer))
			{
				return false;
			}
			if (this.attributeCertificateValid != null && !ix509AttributeCertificate.IsValid(this.attributeCertificateValid.Value))
			{
				return false;
			}
			if (this.targetNames.Count > 0 || this.targetGroups.Count > 0)
			{
				Asn1OctetString extensionValue = ix509AttributeCertificate.GetExtensionValue(X509Extensions.TargetInformation);
				if (extensionValue != null)
				{
					TargetInformation instance;
					try
					{
						instance = TargetInformation.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
					}
					catch (Exception)
					{
						return false;
					}
					Targets[] targetsObjects = instance.GetTargetsObjects();
					if (this.targetNames.Count > 0)
					{
						bool flag = false;
						int num = 0;
						while (num < targetsObjects.Length && !flag)
						{
							Target[] targets = targetsObjects[num].GetTargets();
							for (int i = 0; i < targets.Length; i++)
							{
								GeneralName targetName = targets[i].TargetName;
								if (targetName != null && this.targetNames.Contains(targetName))
								{
									flag = true;
									break;
								}
							}
							num++;
						}
						if (!flag)
						{
							return false;
						}
					}
					if (this.targetGroups.Count <= 0)
					{
						return true;
					}
					bool flag2 = false;
					int num2 = 0;
					while (num2 < targetsObjects.Length && !flag2)
					{
						Target[] targets2 = targetsObjects[num2].GetTargets();
						for (int j = 0; j < targets2.Length; j++)
						{
							GeneralName targetGroup = targets2[j].TargetGroup;
							if (targetGroup != null && this.targetGroups.Contains(targetGroup))
							{
								flag2 = true;
								break;
							}
						}
						num2++;
					}
					if (!flag2)
					{
						return false;
					}
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000334D8 File Offset: 0x000324D8
		public object Clone()
		{
			return new X509AttrCertStoreSelector(this);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x000334E0 File Offset: 0x000324E0
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x000334E8 File Offset: 0x000324E8
		public IX509AttributeCertificate AttributeCert
		{
			get
			{
				return this.attributeCert;
			}
			set
			{
				this.attributeCert = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000334F1 File Offset: 0x000324F1
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x000334F9 File Offset: 0x000324F9
		[Obsolete("Use AttributeCertificateValid instead")]
		public DateTimeObject AttribueCertificateValid
		{
			get
			{
				return this.attributeCertificateValid;
			}
			set
			{
				this.attributeCertificateValid = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00033502 File Offset: 0x00032502
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0003350A File Offset: 0x0003250A
		public DateTimeObject AttributeCertificateValid
		{
			get
			{
				return this.attributeCertificateValid;
			}
			set
			{
				this.attributeCertificateValid = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00033513 File Offset: 0x00032513
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0003351B File Offset: 0x0003251B
		public AttributeCertificateHolder Holder
		{
			get
			{
				return this.holder;
			}
			set
			{
				this.holder = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00033524 File Offset: 0x00032524
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0003352C File Offset: 0x0003252C
		public AttributeCertificateIssuer Issuer
		{
			get
			{
				return this.issuer;
			}
			set
			{
				this.issuer = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00033535 File Offset: 0x00032535
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0003353D File Offset: 0x0003253D
		public BigInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = value;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00033546 File Offset: 0x00032546
		public void AddTargetName(GeneralName name)
		{
			this.targetNames.Add(name);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00033554 File Offset: 0x00032554
		public void AddTargetName(byte[] name)
		{
			this.AddTargetName(GeneralName.GetInstance(Asn1Object.FromByteArray(name)));
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00033567 File Offset: 0x00032567
		public void SetTargetNames(IEnumerable names)
		{
			this.targetNames = this.ExtractGeneralNames(names);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00033576 File Offset: 0x00032576
		public IEnumerable GetTargetNames()
		{
			return new EnumerableProxy(this.targetNames);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00033583 File Offset: 0x00032583
		public void AddTargetGroup(GeneralName group)
		{
			this.targetGroups.Add(group);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00033591 File Offset: 0x00032591
		public void AddTargetGroup(byte[] name)
		{
			this.AddTargetGroup(GeneralName.GetInstance(Asn1Object.FromByteArray(name)));
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000335A4 File Offset: 0x000325A4
		public void SetTargetGroups(IEnumerable names)
		{
			this.targetGroups = this.ExtractGeneralNames(names);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000335B3 File Offset: 0x000325B3
		public IEnumerable GetTargetGroups()
		{
			return new EnumerableProxy(this.targetGroups);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000335C0 File Offset: 0x000325C0
		private ISet ExtractGeneralNames(IEnumerable names)
		{
			ISet set = new HashSet();
			if (names != null)
			{
				foreach (object obj in names)
				{
					if (obj is GeneralName)
					{
						set.Add(obj);
					}
					else
					{
						set.Add(GeneralName.GetInstance(Asn1Object.FromByteArray((byte[])obj)));
					}
				}
			}
			return set;
		}

		// Token: 0x04000815 RID: 2069
		private IX509AttributeCertificate attributeCert;

		// Token: 0x04000816 RID: 2070
		private DateTimeObject attributeCertificateValid;

		// Token: 0x04000817 RID: 2071
		private AttributeCertificateHolder holder;

		// Token: 0x04000818 RID: 2072
		private AttributeCertificateIssuer issuer;

		// Token: 0x04000819 RID: 2073
		private BigInteger serialNumber;

		// Token: 0x0400081A RID: 2074
		private ISet targetNames = new HashSet();

		// Token: 0x0400081B RID: 2075
		private ISet targetGroups = new HashSet();
	}
}
