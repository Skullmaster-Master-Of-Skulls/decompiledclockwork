using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000051 RID: 81
	[CLSCompliant(true)]
	[Serializable]
	public abstract class Asn1Object : ISerializable
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00010634 File Offset: 0x0000F634
		public Asn1Object(Asn1Identifier id)
		{
			this.id = id;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00010650 File Offset: 0x0000F650
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000316 RID: 790
		public abstract void encode(Asn1Encoder enc, Stream out_Renamed);

		// Token: 0x06000317 RID: 791 RVA: 0x00010660 File Offset: 0x0000F660
		public virtual Asn1Identifier getIdentifier()
		{
			return this.id;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00010678 File Offset: 0x0000F678
		public virtual void setIdentifier(Asn1Identifier id)
		{
			this.id = id;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00010690 File Offset: 0x0000F690
		[CLSCompliant(false)]
		public sbyte[] getEncoding(Asn1Encoder enc)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				this.encode(enc, memoryStream);
			}
			catch (IOException ex)
			{
				throw new SystemException("IOException while encoding to byte array: " + ex.ToString());
			}
			return SupportClass.ToSByteArray(memoryStream.ToArray());
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000106F0 File Offset: 0x0000F6F0
		[CLSCompliant(false)]
		public override string ToString()
		{
			string[] array = new string[]
			{
				"[UNIVERSAL ",
				"[APPLICATION ",
				"[",
				"[PRIVATE "
			};
			StringBuilder stringBuilder = new StringBuilder();
			Asn1Identifier identifier = this.getIdentifier();
			stringBuilder.Append(array[identifier.Asn1Class]).Append(identifier.Tag).Append("] ");
			return stringBuilder.ToString();
		}

		// Token: 0x0400017F RID: 383
		private Asn1Identifier id;
	}
}
