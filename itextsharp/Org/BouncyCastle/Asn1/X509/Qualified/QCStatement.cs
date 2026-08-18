using System;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x02000361 RID: 865
	public class QCStatement : Asn1Encodable
	{
		// Token: 0x06001EF1 RID: 7921 RVA: 0x000BA28C File Offset: 0x000B928C
		public static QCStatement GetInstance(object obj)
		{
			if (obj == null || obj is QCStatement)
			{
				return (QCStatement)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new QCStatement(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000BA2DE File Offset: 0x000B92DE
		private QCStatement(Asn1Sequence seq)
		{
			this.qcStatementId = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.qcStatementInfo = seq[1];
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000BA30E File Offset: 0x000B930E
		public QCStatement(DerObjectIdentifier qcStatementId)
		{
			this.qcStatementId = qcStatementId;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000BA31D File Offset: 0x000B931D
		public QCStatement(DerObjectIdentifier qcStatementId, Asn1Encodable qcStatementInfo)
		{
			this.qcStatementId = qcStatementId;
			this.qcStatementInfo = qcStatementInfo;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x000BA333 File Offset: 0x000B9333
		public DerObjectIdentifier StatementId
		{
			get
			{
				return this.qcStatementId;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x000BA33B File Offset: 0x000B933B
		public Asn1Encodable StatementInfo
		{
			get
			{
				return this.qcStatementInfo;
			}
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000BA344 File Offset: 0x000B9344
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.qcStatementId
			});
			if (this.qcStatementInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.qcStatementInfo
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001562 RID: 5474
		private readonly DerObjectIdentifier qcStatementId;

		// Token: 0x04001563 RID: 5475
		private readonly Asn1Encodable qcStatementInfo;
	}
}
