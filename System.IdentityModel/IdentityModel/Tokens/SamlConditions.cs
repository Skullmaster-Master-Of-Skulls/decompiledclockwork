using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000158 RID: 344
	public class SamlConditions
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x0002F938 File Offset: 0x0002DB38
		public SamlConditions()
		{
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002F961 File Offset: 0x0002DB61
		public SamlConditions(DateTime notBefore, DateTime notOnOrAfter) : this(notBefore, notOnOrAfter, null)
		{
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002F96C File Offset: 0x0002DB6C
		public SamlConditions(DateTime notBefore, DateTime notOnOrAfter, IEnumerable<SamlCondition> conditions)
		{
			this.notBefore = notBefore.ToUniversalTime();
			this.notOnOrAfter = notOnOrAfter.ToUniversalTime();
			if (conditions != null)
			{
				foreach (SamlCondition samlCondition in conditions)
				{
					if (samlCondition == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
						{
							XD.SamlDictionary.Condition.Value
						}));
					}
					this.conditions.Add(samlCondition);
				}
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0002FA30 File Offset: 0x0002DC30
		public IList<SamlCondition> Conditions
		{
			get
			{
				return this.conditions;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0002FA38 File Offset: 0x0002DC38
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0002FA40 File Offset: 0x0002DC40
		public DateTime NotBefore
		{
			get
			{
				return this.notBefore;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.notBefore = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002FA6B File Offset: 0x0002DC6B
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0002FA73 File Offset: 0x0002DC73
		public DateTime NotOnOrAfter
		{
			get
			{
				return this.notOnOrAfter;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.notOnOrAfter = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002FA9E File Offset: 0x0002DC9E
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002FAA8 File Offset: 0x0002DCA8
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.conditions.MakeReadOnly();
				foreach (SamlCondition samlCondition in this.conditions)
				{
					samlCondition.MakeReadOnly();
				}
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002FB10 File Offset: 0x0002DD10
		public virtual void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			string attribute = reader.GetAttribute(samlDictionary.NotBefore, null);
			if (!string.IsNullOrEmpty(attribute))
			{
				this.notBefore = DateTime.ParseExact(attribute, SamlConstants.AcceptedDateTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
			}
			attribute = reader.GetAttribute(samlDictionary.NotOnOrAfter, null);
			if (!string.IsNullOrEmpty(attribute))
			{
				this.notOnOrAfter = DateTime.ParseExact(attribute, SamlConstants.AcceptedDateTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
			}
			if (reader.IsEmptyElement)
			{
				reader.MoveToContent();
				reader.Read();
				return;
			}
			reader.MoveToContent();
			reader.Read();
			while (reader.IsStartElement())
			{
				SamlCondition samlCondition = samlSerializer.LoadCondition(reader, keyInfoSerializer, outOfBandTokenResolver);
				if (samlCondition == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLUnableToLoadCondtion")));
				}
				this.conditions.Add(samlCondition);
			}
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002FC34 File Offset: 0x0002DE34
		public virtual void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.Conditions, samlDictionary.Namespace);
			if (this.notBefore != SecurityUtils.MinUtcDateTime)
			{
				writer.WriteStartAttribute(samlDictionary.NotBefore, null);
				writer.WriteString(this.notBefore.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", DateTimeFormatInfo.InvariantInfo));
				writer.WriteEndAttribute();
			}
			if (this.notOnOrAfter != SecurityUtils.MaxUtcDateTime)
			{
				writer.WriteStartAttribute(samlDictionary.NotOnOrAfter, null);
				writer.WriteString(this.notOnOrAfter.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", DateTimeFormatInfo.InvariantInfo));
				writer.WriteEndAttribute();
			}
			for (int i = 0; i < this.conditions.Count; i++)
			{
				this.conditions[i].WriteXml(writer, samlSerializer, keyInfoSerializer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000BC1 RID: 3009
		private readonly ImmutableCollection<SamlCondition> conditions = new ImmutableCollection<SamlCondition>();

		// Token: 0x04000BC2 RID: 3010
		private bool isReadOnly;

		// Token: 0x04000BC3 RID: 3011
		private DateTime notBefore = SecurityUtils.MinUtcDateTime;

		// Token: 0x04000BC4 RID: 3012
		private DateTime notOnOrAfter = SecurityUtils.MaxUtcDateTime;
	}
}
