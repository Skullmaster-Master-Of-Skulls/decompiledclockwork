using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000515 RID: 1301
	public class GeneralNames : Asn1Encodable
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x0010EE0C File Offset: 0x0010DE0C
		public static GeneralNames GetInstance(object obj)
		{
			if (obj == null || obj is GeneralNames)
			{
				return (GeneralNames)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new GeneralNames((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x0010EE5E File Offset: 0x0010DE5E
		public static GeneralNames GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return GeneralNames.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0010EE6C File Offset: 0x0010DE6C
		public GeneralNames(GeneralName name)
		{
			this.names = new GeneralName[]
			{
				name
			};
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x0010EE94 File Offset: 0x0010DE94
		private GeneralNames(Asn1Sequence seq)
		{
			this.names = new GeneralName[seq.Count];
			for (int num = 0; num != seq.Count; num++)
			{
				this.names[num] = GeneralName.GetInstance(seq[num]);
			}
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x0010EEDD File Offset: 0x0010DEDD
		public GeneralName[] GetNames()
		{
			return (GeneralName[])this.names.Clone();
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x0010EEEF File Offset: 0x0010DEEF
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(this.names);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x0010EEFC File Offset: 0x0010DEFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("GeneralNames:");
			stringBuilder.Append(newLine);
			foreach (GeneralName value in this.names)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(value);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001EA3 RID: 7843
		private readonly GeneralName[] names;
	}
}
