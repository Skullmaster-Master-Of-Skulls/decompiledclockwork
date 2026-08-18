using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001C RID: 28
	public class LdapAttribute : ICloneable, IComparable
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006094 File Offset: 0x00005094
		public virtual IEnumerator ByteValues
		{
			get
			{
				return new ArrayEnumeration(this.ByteValueArray);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000060B0 File Offset: 0x000050B0
		public virtual IEnumerator StringValues
		{
			get
			{
				return new ArrayEnumeration(this.StringValueArray);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000060CC File Offset: 0x000050CC
		[CLSCompliant(false)]
		public virtual sbyte[][] ByteValueArray
		{
			get
			{
				sbyte[][] result;
				if (this.values == null)
				{
					result = new sbyte[0][];
				}
				else
				{
					int num = this.values.Length;
					sbyte[][] array = new sbyte[num][];
					int i = 0;
					int num2 = num;
					while (i < num2)
					{
						array[i] = new sbyte[((sbyte[])this.values[i]).Length];
						Array.Copy((Array)this.values[i], 0, array[i], 0, array[i].Length);
						i++;
					}
					result = array;
				}
				return result;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006144 File Offset: 0x00005144
		public virtual string[] StringValueArray
		{
			get
			{
				string[] result;
				if (this.values == null)
				{
					result = new string[0];
				}
				else
				{
					int num = this.values.Length;
					string[] array = new string[num];
					for (int i = 0; i < num; i++)
					{
						try
						{
							Encoding encoding = Encoding.GetEncoding("utf-8");
							char[] chars = encoding.GetChars(SupportClass.ToByteArray((sbyte[])this.values[i]));
							array[i] = new string(chars);
						}
						catch (IOException ex)
						{
							throw new SystemException(ex.ToString());
						}
					}
					result = array;
				}
				return result;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000061E4 File Offset: 0x000051E4
		public virtual string StringValue
		{
			get
			{
				string result = null;
				if (this.values != null)
				{
					try
					{
						Encoding encoding = Encoding.GetEncoding("utf-8");
						char[] chars = encoding.GetChars(SupportClass.ToByteArray((sbyte[])this.values[0]));
						result = new string(chars);
					}
					catch (IOException ex)
					{
						throw new SystemException(ex.ToString());
					}
				}
				return result;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006258 File Offset: 0x00005258
		[CLSCompliant(false)]
		public virtual sbyte[] ByteValue
		{
			get
			{
				sbyte[] array = null;
				if (this.values != null)
				{
					array = new sbyte[((sbyte[])this.values[0]).Length];
					Array.Copy((Array)this.values[0], 0, array, 0, array.Length);
				}
				return array;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000062A4 File Offset: 0x000052A4
		public virtual string LangSubtype
		{
			get
			{
				if (this.subTypes != null)
				{
					for (int i = 0; i < this.subTypes.Length; i++)
					{
						if (this.subTypes[i].StartsWith("lang-"))
						{
							return this.subTypes[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000062F0 File Offset: 0x000052F0
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000030 RID: 48
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00006308 File Offset: 0x00005308
		protected internal virtual string Value
		{
			set
			{
				this.values = null;
				try
				{
					Encoding encoding = Encoding.GetEncoding("utf-8");
					byte[] bytes = encoding.GetBytes(value);
					sbyte[] bytes2 = SupportClass.ToSByteArray(bytes);
					this.add(bytes2);
				}
				catch (IOException ex)
				{
					throw new SystemException(ex.ToString());
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000636C File Offset: 0x0000536C
		public LdapAttribute(LdapAttribute attr)
		{
			if (attr == null)
			{
				throw new ArgumentException("LdapAttribute class cannot be null");
			}
			this.name = attr.name;
			this.baseName = attr.baseName;
			if (attr.subTypes != null)
			{
				this.subTypes = new string[attr.subTypes.Length];
				Array.Copy(attr.subTypes, 0, this.subTypes, 0, this.subTypes.Length);
			}
			if (attr.values != null)
			{
				this.values = new object[attr.values.Length];
				Array.Copy(attr.values, 0, this.values, 0, this.values.Length);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006424 File Offset: 0x00005424
		public LdapAttribute(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			this.name = attrName;
			this.baseName = LdapAttribute.getBaseName(attrName);
			this.subTypes = LdapAttribute.getSubtypes(attrName);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006474 File Offset: 0x00005474
		[CLSCompliant(false)]
		public LdapAttribute(string attrName, sbyte[] attrBytes) : this(attrName)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			sbyte[] array = new sbyte[attrBytes.Length];
			Array.Copy(attrBytes, 0, array, 0, attrBytes.Length);
			this.add(array);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000064B4 File Offset: 0x000054B4
		public LdapAttribute(string attrName, string attrString) : this(attrName)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				Encoding encoding = Encoding.GetEncoding("utf-8");
				byte[] bytes = encoding.GetBytes(attrString);
				sbyte[] bytes2 = SupportClass.ToSByteArray(bytes);
				this.add(bytes2);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006524 File Offset: 0x00005524
		public LdapAttribute(string attrName, string[] attrStrings) : this(attrName)
		{
			if (attrStrings == null)
			{
				throw new ArgumentException("Attribute values array cannot be null");
			}
			int i = 0;
			int num = attrStrings.Length;
			while (i < num)
			{
				try
				{
					if (attrStrings[i] == null)
					{
						throw new ArgumentException("Attribute value at array index " + i + " cannot be null");
					}
					Encoding encoding = Encoding.GetEncoding("utf-8");
					byte[] bytes = encoding.GetBytes(attrStrings[i]);
					sbyte[] bytes2 = SupportClass.ToSByteArray(bytes);
					this.add(bytes2);
				}
				catch (IOException ex)
				{
					throw new SystemException(ex.ToString());
				}
				i++;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000065CC File Offset: 0x000055CC
		public object Clone()
		{
			object result;
			try
			{
				object obj = base.MemberwiseClone();
				if (this.values != null)
				{
					Array.Copy(this.values, 0, ((LdapAttribute)obj).values, 0, this.values.Length);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return result;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006638 File Offset: 0x00005638
		public virtual void addValue(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				Encoding encoding = Encoding.GetEncoding("utf-8");
				byte[] bytes = encoding.GetBytes(attrString);
				sbyte[] bytes2 = SupportClass.ToSByteArray(bytes);
				this.add(bytes2);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000066A4 File Offset: 0x000056A4
		[CLSCompliant(false)]
		public virtual void addValue(sbyte[] attrBytes)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(attrBytes);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000066C8 File Offset: 0x000056C8
		public virtual void addBase64Value(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrString));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000066F4 File Offset: 0x000056F4
		public virtual void addBase64Value(StringBuilder attrString, int start, int end)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrString, start, end));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006720 File Offset: 0x00005720
		public virtual void addBase64Value(char[] attrChars)
		{
			if (attrChars == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			this.add(Base64.decode(attrChars));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000674C File Offset: 0x0000574C
		public virtual void addURLValue(string url)
		{
			if (url == null)
			{
				throw new ArgumentException("Attribute URL cannot be null");
			}
			this.addURLValue(new Uri(url));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006778 File Offset: 0x00005778
		public virtual void addURLValue(Uri url)
		{
			if (url == null)
			{
				throw new ArgumentException("Attribute URL cannot be null");
			}
			try
			{
				Stream responseStream = WebRequest.Create(url).GetResponse().GetResponseStream();
				ArrayList arrayList = new ArrayList();
				sbyte[] data = new sbyte[4096];
				int num = 0;
				int num2;
				while ((num2 = SupportClass.ReadInput(responseStream, ref data, 0, 4096)) != -1)
				{
					arrayList.Add(new LdapAttribute.URLData(this, data, num2));
					data = new sbyte[4096];
					num += num2;
				}
				sbyte[] array = new sbyte[num];
				int num3 = 0;
				for (int i = 0; i < arrayList.Count; i++)
				{
					LdapAttribute.URLData urldata = (LdapAttribute.URLData)arrayList[i];
					num2 = urldata.getLength();
					Array.Copy(urldata.getData(), 0, array, num3, num2);
					num3 += num2;
				}
				this.add(array);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006874 File Offset: 0x00005874
		public virtual string getBaseName()
		{
			return this.baseName;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000688C File Offset: 0x0000588C
		public static string getBaseName(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			int num = attrName.IndexOf(';');
			string result;
			if (-1 == num)
			{
				result = attrName;
			}
			else
			{
				result = attrName.Substring(0, num);
			}
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000068C4 File Offset: 0x000058C4
		public virtual string[] getSubtypes()
		{
			return this.subTypes;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000068DC File Offset: 0x000058DC
		public static string[] getSubtypes(string attrName)
		{
			if (attrName == null)
			{
				throw new ArgumentException("Attribute name cannot be null");
			}
			SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(attrName, ";");
			string[] array = null;
			int count = tokenizer.Count;
			if (count > 0)
			{
				tokenizer.NextToken();
				array = new string[count - 1];
				int num = 0;
				while (tokenizer.HasMoreTokens())
				{
					array[num++] = tokenizer.NextToken();
				}
			}
			return array;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006940 File Offset: 0x00005940
		public virtual bool hasSubtype(string subtype)
		{
			if (subtype == null)
			{
				throw new ArgumentException("subtype cannot be null");
			}
			if (this.subTypes != null)
			{
				for (int i = 0; i < this.subTypes.Length; i++)
				{
					if (this.subTypes[i].ToUpper().Equals(subtype.ToUpper()))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000699C File Offset: 0x0000599C
		public virtual bool hasSubtypes(string[] subtypes)
		{
			if (subtypes == null)
			{
				throw new ArgumentException("subtypes cannot be null");
			}
			int i = 0;
			IL_70:
			while (i < subtypes.Length)
			{
				for (int j = 0; j < this.subTypes.Length; j++)
				{
					if (this.subTypes[j] == null)
					{
						throw new ArgumentException("subtype at array index " + i + " cannot be null");
					}
					if (this.subTypes[j].ToUpper().Equals(subtypes[i].ToUpper()))
					{
						i++;
						goto IL_70;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006A24 File Offset: 0x00005A24
		public virtual void removeValue(string attrString)
		{
			if (attrString == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			try
			{
				Encoding encoding = Encoding.GetEncoding("utf-8");
				byte[] bytes = encoding.GetBytes(attrString);
				sbyte[] attrBytes = SupportClass.ToSByteArray(bytes);
				this.removeValue(attrBytes);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006A90 File Offset: 0x00005A90
		[CLSCompliant(false)]
		public virtual void removeValue(sbyte[] attrBytes)
		{
			if (attrBytes == null)
			{
				throw new ArgumentException("Attribute value cannot be null");
			}
			int i = 0;
			while (i < this.values.Length)
			{
				if (this.equals(attrBytes, (sbyte[])this.values[i]))
				{
					if (i == 0 && 1 == this.values.Length)
					{
						this.values = null;
						return;
					}
					if (this.values.Length == 1)
					{
						this.values = null;
					}
					else
					{
						int num = this.values.Length - i - 1;
						object[] destinationArray = new object[this.values.Length - 1];
						if (i != 0)
						{
							Array.Copy(this.values, 0, destinationArray, 0, i);
						}
						if (num != 0)
						{
							Array.Copy(this.values, i + 1, destinationArray, i, num);
						}
						this.values = destinationArray;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006B58 File Offset: 0x00005B58
		public virtual int size()
		{
			return (this.values == null) ? 0 : this.values.Length;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006B7C File Offset: 0x00005B7C
		public virtual int CompareTo(object attribute)
		{
			return this.name.CompareTo(((LdapAttribute)attribute).name);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006BA4 File Offset: 0x00005BA4
		private void add(sbyte[] bytes)
		{
			if (this.values == null)
			{
				this.values = new object[]
				{
					bytes
				};
			}
			else
			{
				for (int i = 0; i < this.values.Length; i++)
				{
					if (this.equals(bytes, (sbyte[])this.values[i]))
					{
						return;
					}
				}
				object[] array = new object[this.values.Length + 1];
				Array.Copy(this.values, 0, array, 0, this.values.Length);
				array[this.values.Length] = bytes;
				this.values = array;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006C34 File Offset: 0x00005C34
		private bool equals(sbyte[] e1, sbyte[] e2)
		{
			bool result;
			if (e1 == e2)
			{
				result = true;
			}
			else if (e1 == null || e2 == null)
			{
				result = false;
			}
			else
			{
				int num = e1.Length;
				if (e2.Length != num)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < num; i++)
					{
						if (e1[i] != e2[i])
						{
							return false;
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006C80 File Offset: 0x00005C80
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapAttribute: ");
			try
			{
				stringBuilder.Append("{type='" + this.name + "'");
				if (this.values != null)
				{
					stringBuilder.Append(", ");
					if (this.values.Length == 1)
					{
						stringBuilder.Append("value='");
					}
					else
					{
						stringBuilder.Append("values='");
					}
					for (int i = 0; i < this.values.Length; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append("','");
						}
						if (((sbyte[])this.values[i]).Length != 0)
						{
							Encoding encoding = Encoding.GetEncoding("utf-8");
							char[] chars = encoding.GetChars(SupportClass.ToByteArray((sbyte[])this.values[i]));
							string text = new string(chars);
							if (text.Length == 0)
							{
								stringBuilder.Append("<binary value, length:" + text.Length);
							}
							else
							{
								stringBuilder.Append(text);
							}
						}
					}
					stringBuilder.Append("'");
				}
				stringBuilder.Append("}");
			}
			catch (Exception ex)
			{
				throw new SystemException(ex.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000A5 RID: 165
		private string name;

		// Token: 0x040000A6 RID: 166
		private string baseName;

		// Token: 0x040000A7 RID: 167
		private string[] subTypes = null;

		// Token: 0x040000A8 RID: 168
		private object[] values = null;

		// Token: 0x0200001D RID: 29
		private class URLData
		{
			// Token: 0x06000128 RID: 296 RVA: 0x00006DDC File Offset: 0x00005DDC
			private void InitBlock(LdapAttribute enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000129 RID: 297 RVA: 0x00006DF0 File Offset: 0x00005DF0
			public LdapAttribute Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x0600012A RID: 298 RVA: 0x00006E08 File Offset: 0x00005E08
			public URLData(LdapAttribute enclosingInstance, sbyte[] data, int length)
			{
				this.InitBlock(enclosingInstance);
				this.length = length;
				this.data = data;
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00006E34 File Offset: 0x00005E34
			public int getLength()
			{
				return this.length;
			}

			// Token: 0x0600012C RID: 300 RVA: 0x00006E4C File Offset: 0x00005E4C
			public sbyte[] getData()
			{
				return this.data;
			}

			// Token: 0x040000A9 RID: 169
			private LdapAttribute enclosingInstance;

			// Token: 0x040000AA RID: 170
			private int length;

			// Token: 0x040000AB RID: 171
			private sbyte[] data;
		}
	}
}
