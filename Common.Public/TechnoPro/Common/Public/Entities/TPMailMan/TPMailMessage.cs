using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Public.Entities.TPMailMan
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public class TPMailMessage : BusinessBase<string>, ICloneable<TPMailMessage>, ICloneable
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x00011C14 File Offset: 0x0000FE14
		public TPMailMessage()
		{
			this.UniqueId = Guid.NewGuid().ToString();
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00011C43 File Offset: 0x0000FE43
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00011C4B File Offset: 0x0000FE4B
		public List<TPMailAddress> To { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00011C54 File Offset: 0x0000FE54
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x00011C5C File Offset: 0x0000FE5C
		public List<TPMailAddress> Cc { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00011C65 File Offset: 0x0000FE65
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00011C6D File Offset: 0x0000FE6D
		public List<TPMailAddress> Bcc { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00011C76 File Offset: 0x0000FE76
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00011C7E File Offset: 0x0000FE7E
		public TPMailAddress From { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00011C87 File Offset: 0x0000FE87
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x00011C8F File Offset: 0x0000FE8F
		public string Subject { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00011C98 File Offset: 0x0000FE98
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		public string Body { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00011CA9 File Offset: 0x0000FEA9
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00011CB1 File Offset: 0x0000FEB1
		public eEmailBodyType BodyType { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00011CBA File Offset: 0x0000FEBA
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00011CC2 File Offset: 0x0000FEC2
		[Obsolete("Only Body and BodyType will be used, unless BodyType is missing then will use Body or BodyHtml.  Don't use BodyHtml anymore.")]
		public string BodyHtml { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00011CCB File Offset: 0x0000FECB
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x00011CD3 File Offset: 0x0000FED3
		public List<TPMailAttachment> Attachments { get; set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00011CDC File Offset: 0x0000FEDC
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		public eTPMessageDeliveryMethod DeliveryMethod { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00011CED File Offset: 0x0000FEED
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x00011CF5 File Offset: 0x0000FEF5
		public eTPMessagePriority Priority { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00011CFE File Offset: 0x0000FEFE
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00011D06 File Offset: 0x0000FF06
		public bool IsActive { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00011D0F File Offset: 0x0000FF0F
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00011D17 File Offset: 0x0000FF17
		public string ErrorMessage { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00011D20 File Offset: 0x0000FF20
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00011D28 File Offset: 0x0000FF28
		public string ErrorMessageHtml { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00011D31 File Offset: 0x0000FF31
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x00011D39 File Offset: 0x0000FF39
		public bool WasSent { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00011D44 File Offset: 0x0000FF44
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string UniqueId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public string GetPlainTextBody()
		{
			eEmailBodyType bodyType = this.BodyType;
			eEmailBodyType eEmailBodyType = bodyType;
			string result;
			if (eEmailBodyType != eEmailBodyType.PlainText)
			{
				if (eEmailBodyType != eEmailBodyType.Html)
				{
					result = (this.Body ?? "");
				}
				else
				{
					result = null;
				}
			}
			else
			{
				result = (this.Body ?? "");
			}
			return result;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00011DA8 File Offset: 0x0000FFA8
		public string GetHtmlBody()
		{
			eEmailBodyType bodyType = this.BodyType;
			eEmailBodyType eEmailBodyType = bodyType;
			string result;
			if (eEmailBodyType != eEmailBodyType.PlainText)
			{
				if (eEmailBodyType != eEmailBodyType.Html)
				{
					result = (string.IsNullOrEmpty(this.BodyHtml) ? (this.Body ?? "") : this.BodyHtml);
				}
				else
				{
					result = (this.BodyHtml ?? "").DecodeHtml();
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00011E10 File Offset: 0x00010010
		public TPMailMessage Clone()
		{
			return new TPMailMessage(this);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00011E28 File Offset: 0x00010028
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00011E40 File Offset: 0x00010040
		public TPMailMessage(TPMailMessage item)
		{
			bool flag = item == null;
			if (!flag)
			{
				List<TPMailAddress> to;
				if (item.To != null)
				{
					to = item.To.ConvertAll<TPMailAddress>((TPMailAddress g) => g.Clone());
				}
				else
				{
					to = null;
				}
				this.To = to;
				List<TPMailAddress> cc;
				if (item.Cc != null)
				{
					cc = item.Cc.ConvertAll<TPMailAddress>((TPMailAddress g) => g.Clone());
				}
				else
				{
					cc = null;
				}
				this.Cc = cc;
				List<TPMailAddress> bcc;
				if (item.Bcc != null)
				{
					bcc = item.Bcc.ConvertAll<TPMailAddress>((TPMailAddress g) => g.Clone());
				}
				else
				{
					bcc = null;
				}
				this.Bcc = bcc;
				this.From = ((item.From == null) ? null : item.From.Clone());
				this.Subject = item.Subject;
				this.Body = item.Body;
				this.BodyType = item.BodyType;
				this.BodyHtml = item.BodyHtml;
				this.DeliveryMethod = item.DeliveryMethod;
				this.IsActive = item.IsActive;
				this.Priority = item.Priority;
				this.ErrorMessage = item.ErrorMessage;
				this.ErrorMessageHtml = item.ErrorMessageHtml;
				this.WasSent = item.WasSent;
				List<TPMailAttachment> attachments;
				if (item.Attachments != null)
				{
					attachments = item.Attachments.ToList<TPMailAttachment>().ConvertAll<TPMailAttachment>((TPMailAttachment g) => g.Clone());
				}
				else
				{
					attachments = null;
				}
				this.Attachments = attachments;
				this.UniqueId = item.UniqueId;
			}
		}
	}
}
