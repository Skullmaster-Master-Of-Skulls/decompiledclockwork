using System;
using System.Text;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001A6 RID: 422
	public class RoleSyntax : Asn1Encodable
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x0005D530 File Offset: 0x0005C530
		public static RoleSyntax GetInstance(object obj)
		{
			if (obj == null || obj is RoleSyntax)
			{
				return (RoleSyntax)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RoleSyntax((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in 'RoleSyntax' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0005D584 File Offset: 0x0005C584
		public RoleSyntax(GeneralNames roleAuthority, GeneralName roleName)
		{
			if (roleName == null || roleName.TagNo != 6 || ((IAsn1String)roleName.Name).GetString().Equals(""))
			{
				throw new ArgumentException("the role name MUST be non empty and MUST use the URI option of GeneralName");
			}
			this.roleAuthority = roleAuthority;
			this.roleName = roleName;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0005D5D8 File Offset: 0x0005C5D8
		public RoleSyntax(GeneralName roleName) : this(null, roleName)
		{
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0005D5E2 File Offset: 0x0005C5E2
		public RoleSyntax(string roleName) : this(new GeneralName(6, (roleName == null) ? "" : roleName))
		{
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0005D5FC File Offset: 0x0005C5FC
		private RoleSyntax(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this.roleAuthority = GeneralNames.GetInstance(instance, false);
					break;
				case 1:
					this.roleName = GeneralName.GetInstance(instance, true);
					break;
				default:
					throw new ArgumentException("Unknown tag in RoleSyntax");
				}
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0005D69A File Offset: 0x0005C69A
		public GeneralNames RoleAuthority
		{
			get
			{
				return this.roleAuthority;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0005D6A2 File Offset: 0x0005C6A2
		public GeneralName RoleName
		{
			get
			{
				return this.roleName;
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0005D6AA File Offset: 0x0005C6AA
		public string GetRoleNameAsString()
		{
			return ((IAsn1String)this.roleName.Name).GetString();
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0005D6C4 File Offset: 0x0005C6C4
		public string[] GetRoleAuthorityAsString()
		{
			if (this.roleAuthority == null)
			{
				return new string[0];
			}
			GeneralName[] names = this.roleAuthority.GetNames();
			string[] array = new string[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				Asn1Encodable name = names[i].Name;
				if (name is IAsn1String)
				{
					array[i] = ((IAsn1String)name).GetString();
				}
				else
				{
					array[i] = name.ToString();
				}
			}
			return array;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0005D730 File Offset: 0x0005C730
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.roleAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.roleAuthority)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerTaggedObject(true, 1, this.roleName)
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0005D794 File Offset: 0x0005C794
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Name: " + this.GetRoleNameAsString() + " - Auth: ");
			if (this.roleAuthority == null || this.roleAuthority.GetNames().Length == 0)
			{
				stringBuilder.Append("N/A");
			}
			else
			{
				string[] roleAuthorityAsString = this.GetRoleAuthorityAsString();
				stringBuilder.Append('[').Append(roleAuthorityAsString[0]);
				for (int i = 1; i < roleAuthorityAsString.Length; i++)
				{
					stringBuilder.Append(", ").Append(roleAuthorityAsString[i]);
				}
				stringBuilder.Append(']');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BE0 RID: 3040
		private readonly GeneralNames roleAuthority;

		// Token: 0x04000BE1 RID: 3041
		private readonly GeneralName roleName;
	}
}
