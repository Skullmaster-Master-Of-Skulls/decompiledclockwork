using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;
using Telerik.Web.Dialogs;

namespace Telerik.Web.UI
{
	// Token: 0x02001037 RID: 4151
	[Serializable]
	public class DialogParameters : Hashtable
	{
		// Token: 0x0600A380 RID: 41856 RVA: 0x0024625C File Offset: 0x0024445C
		internal virtual void Add(XmlNode toolsFileNode)
		{
			XmlAttribute xmlAttribute = toolsFileNode.Attributes["name"];
			XmlAttribute xmlAttribute2 = toolsFileNode.Attributes["value"];
			if (xmlAttribute != null && xmlAttribute2 != null)
			{
				this.Add(xmlAttribute.Value, xmlAttribute2.Value);
			}
		}

		// Token: 0x0600A381 RID: 41857 RVA: 0x002462A3 File Offset: 0x002444A3
		public DialogParameters()
		{
		}

		// Token: 0x0600A382 RID: 41858 RVA: 0x002462AB File Offset: 0x002444AB
		protected DialogParameters(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A383 RID: 41859 RVA: 0x002462B5 File Offset: 0x002444B5
		internal string Serialize()
		{
			return new DialogParametersSerializer(this).Result;
		}

		// Token: 0x0600A384 RID: 41860 RVA: 0x002462C4 File Offset: 0x002444C4
		internal static DialogParameters Deserialize(string source)
		{
			DialogParameters dialogParameters = DialogParametersSerializer.Deserialize(source);
			if (dialogParameters == null)
			{
				throw new ArgumentException("The Dialog parameters are corrupted.");
			}
			return dialogParameters;
		}
	}
}
