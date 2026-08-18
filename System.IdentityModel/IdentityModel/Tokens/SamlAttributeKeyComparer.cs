using System;
using System.Collections.Generic;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200014F RID: 335
	internal class SamlAttributeKeyComparer : IEqualityComparer<SamlAttributeKeyComparer.AttributeKey>
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
		public bool Equals(SamlAttributeKeyComparer.AttributeKey x, SamlAttributeKeyComparer.AttributeKey y)
		{
			return x.Name.Equals(y.Name, StringComparison.Ordinal) && x.FriendlyName.Equals(y.FriendlyName, StringComparison.Ordinal) && x.ValueType.Equals(y.ValueType, StringComparison.Ordinal) && x.OriginalIssuer.Equals(y.OriginalIssuer, StringComparison.Ordinal) && x.NameFormat.Equals(y.NameFormat, StringComparison.Ordinal) && x.Namespace.Equals(y.Namespace, StringComparison.Ordinal);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002DB5D File Offset: 0x0002BD5D
		public int GetHashCode(SamlAttributeKeyComparer.AttributeKey obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x0200026A RID: 618
		public class AttributeKey
		{
			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x0600127B RID: 4731 RVA: 0x00050528 File Offset: 0x0004E728
			internal string FriendlyName
			{
				get
				{
					return this._friendlyName;
				}
			}

			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x0600127C RID: 4732 RVA: 0x00050530 File Offset: 0x0004E730
			internal string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x1700052B RID: 1323
			// (get) Token: 0x0600127D RID: 4733 RVA: 0x00050538 File Offset: 0x0004E738
			internal string NameFormat
			{
				get
				{
					return this._nameFormat;
				}
			}

			// Token: 0x1700052C RID: 1324
			// (get) Token: 0x0600127E RID: 4734 RVA: 0x00050540 File Offset: 0x0004E740
			internal string Namespace
			{
				get
				{
					return this._namespace;
				}
			}

			// Token: 0x1700052D RID: 1325
			// (get) Token: 0x0600127F RID: 4735 RVA: 0x00050548 File Offset: 0x0004E748
			internal string ValueType
			{
				get
				{
					return this._valueType;
				}
			}

			// Token: 0x1700052E RID: 1326
			// (get) Token: 0x06001280 RID: 4736 RVA: 0x00050550 File Offset: 0x0004E750
			internal string OriginalIssuer
			{
				get
				{
					return this._originalIssuer;
				}
			}

			// Token: 0x06001281 RID: 4737 RVA: 0x00050558 File Offset: 0x0004E758
			public AttributeKey(SamlAttribute attribute)
			{
				if (attribute == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attribute");
				}
				this._friendlyName = string.Empty;
				this._name = attribute.Name;
				this._nameFormat = string.Empty;
				this._namespace = (attribute.Namespace ?? string.Empty);
				this._valueType = (attribute.AttributeValueXsiType ?? string.Empty);
				this._originalIssuer = (attribute.OriginalIssuer ?? string.Empty);
				this.ComputeHashCode();
			}

			// Token: 0x06001282 RID: 4738 RVA: 0x000505E8 File Offset: 0x0004E7E8
			public AttributeKey(Saml2Attribute attribute)
			{
				if (attribute == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attribute");
				}
				this._friendlyName = (attribute.FriendlyName ?? string.Empty);
				this._name = attribute.Name;
				this._nameFormat = ((attribute.NameFormat == null) ? string.Empty : attribute.NameFormat.AbsoluteUri);
				this._namespace = string.Empty;
				this._valueType = (attribute.AttributeValueXsiType ?? string.Empty);
				this._originalIssuer = (attribute.OriginalIssuer ?? string.Empty);
				this.ComputeHashCode();
			}

			// Token: 0x06001283 RID: 4739 RVA: 0x00050690 File Offset: 0x0004E890
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x06001284 RID: 4740 RVA: 0x00050698 File Offset: 0x0004E898
			private void ComputeHashCode()
			{
				this._hashCode = this._name.GetHashCode();
				this._hashCode ^= this._friendlyName.GetHashCode();
				this._hashCode ^= this._nameFormat.GetHashCode();
				this._hashCode ^= this._namespace.GetHashCode();
				this._hashCode ^= this._valueType.GetHashCode();
				this._hashCode ^= this._originalIssuer.GetHashCode();
			}

			// Token: 0x040010CE RID: 4302
			private string _friendlyName;

			// Token: 0x040010CF RID: 4303
			private int _hashCode;

			// Token: 0x040010D0 RID: 4304
			private string _name;

			// Token: 0x040010D1 RID: 4305
			private string _nameFormat;

			// Token: 0x040010D2 RID: 4306
			private string _namespace;

			// Token: 0x040010D3 RID: 4307
			private string _valueType;

			// Token: 0x040010D4 RID: 4308
			private string _originalIssuer;
		}
	}
}
