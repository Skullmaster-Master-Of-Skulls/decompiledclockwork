using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.Core.Messaging
{
	// Token: 0x020000B1 RID: 177
	public static class DropBoxAdapter
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x000265D8 File Offset: 0x000247D8
		public static Dictionary<string, string> GetEmailParameters(this DropBox_IM im)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"imfrom",
					im.From.Username
				},
				{
					"imto",
					im.To
				},
				{
					"imissuedon",
					im.IssuedOn.ToString(DropBoxAdapter.DATETIME_FORMAT)
				},
				{
					"immessage",
					im.Message
				},
				{
					"imrequiredresponse",
					im.RequiredResponse.ToString()
				}
			};
			bool flag = im.Parameters != null;
			if (flag)
			{
				foreach (KeyValuePair<string, string> keyValuePair in im.Parameters)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000266D0 File Offset: 0x000248D0
		public static Dictionary<string, string> GetEmailParameters(this DropBox_AttachmentInfo att)
		{
			return new Dictionary<string, string>
			{
				{
					"attfrom",
					att.From.Username
				},
				{
					"attto",
					att.To
				},
				{
					"attissuedon",
					att.IssuedOn.ToString(DropBoxAdapter.DATETIME_FORMAT)
				},
				{
					"attfilename",
					att.Filename
				},
				{
					"attextension",
					att.Extension
				},
				{
					"attdescription",
					att.Description
				},
				{
					"attsize",
					att.SizeInBytes.ToString()
				}
			};
		}

		// Token: 0x04000141 RID: 321
		internal static string DATETIME_FORMAT = "MMM d, yyyy hh:mm:ss tt";
	}
}
