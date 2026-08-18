using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Security.Cryptography;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x020002CB RID: 715
	public sealed class ObjectStateFormatter : IStateFormatter, IStateFormatter2, IFormatter
	{
		// Token: 0x0600202D RID: 8237 RVA: 0x00066855 File Offset: 0x00064A55
		public ObjectStateFormatter() : this(null)
		{
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0006685E File Offset: 0x00064A5E
		internal ObjectStateFormatter(byte[] macEncodingKey) : this(null, true)
		{
			this._macKeyBytes = macEncodingKey;
			if (macEncodingKey != null)
			{
				this._forceLegacyCryptography = true;
			}
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00066879 File Offset: 0x00064A79
		internal ObjectStateFormatter(Page page, bool throwOnErrorDeserializing)
		{
			this._page = page;
			this._throwOnErrorDeserializing = throwOnErrorDeserializing;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00066890 File Offset: 0x00064A90
		internal List<string> GetSpecificPurposes()
		{
			if (this._specificPurposes == null)
			{
				if (this._page == null)
				{
					return null;
				}
				List<string> list = new List<string>
				{
					"TemplateSourceDirectory: " + this._page.TemplateSourceDirectory.ToUpperInvariant(),
					"Type: " + this._page.GetType().Name.ToUpperInvariant()
				};
				if (this._page.ViewStateUserKey != null)
				{
					list.Add("ViewStateUserKey: " + this._page.ViewStateUserKey);
				}
				this._specificPurposes = list;
			}
			return this._specificPurposes;
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00066934 File Offset: 0x00064B34
		private byte[] GetMacKeyModifier()
		{
			if (this._macKeyBytes == null)
			{
				if (this._page == null)
				{
					return null;
				}
				uint clientStateIdentifier = this._page.GetClientStateIdentifier();
				string viewStateUserKey = this._page.ViewStateUserKey;
				if (viewStateUserKey != null)
				{
					int byteCount = Encoding.Unicode.GetByteCount(viewStateUserKey);
					this._macKeyBytes = new byte[byteCount + 4];
					Encoding.Unicode.GetBytes(viewStateUserKey, 0, viewStateUserKey.Length, this._macKeyBytes, 4);
				}
				else
				{
					this._macKeyBytes = new byte[4];
				}
				this._macKeyBytes[0] = (byte)clientStateIdentifier;
				this._macKeyBytes[1] = (byte)(clientStateIdentifier >> 8);
				this._macKeyBytes[2] = (byte)(clientStateIdentifier >> 16);
				this._macKeyBytes[3] = (byte)(clientStateIdentifier >> 24);
			}
			return this._macKeyBytes;
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000669E9 File Offset: 0x00064BE9
		private void AddDeserializationStringReference(string s)
		{
			if (this._stringTableCount == 255)
			{
				this._stringTableCount = 0;
			}
			this._stringList[this._stringTableCount] = s;
			this._stringTableCount++;
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00066A1B File Offset: 0x00064C1B
		private void AddDeserializationTypeReference(Type type)
		{
			this._typeList.Add(type);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00066A2C File Offset: 0x00064C2C
		private void AddSerializationStringReference(string s)
		{
			if (this._stringTableCount == 255)
			{
				this._stringTableCount = 0;
			}
			string text = this._stringList[this._stringTableCount];
			if (text != null)
			{
				this._stringTable.Remove(text);
			}
			this._stringTable[s] = this._stringTableCount;
			this._stringList[this._stringTableCount] = s;
			this._stringTableCount++;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00066AA0 File Offset: 0x00064CA0
		private void AddSerializationTypeReference(Type type)
		{
			int count = this._typeTable.Count;
			this._typeTable[type] = count;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00066ACB File Offset: 0x00064CCB
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal object DeserializeWithAssert(Stream inputStream)
		{
			return this.Deserialize(inputStream);
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00066AD4 File Offset: 0x00064CD4
		public object Deserialize(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			Exception innerException = null;
			this.InitializeDeserializer();
			ObjectStateFormatter.SerializerBinaryReader serializerBinaryReader = new ObjectStateFormatter.SerializerBinaryReader(inputStream);
			try
			{
				byte b = serializerBinaryReader.ReadByte();
				if (b == 255)
				{
					byte b2 = serializerBinaryReader.ReadByte();
					if (b2 == 1)
					{
						return this.DeserializeValue(serializerBinaryReader);
					}
				}
			}
			catch (Exception ex)
			{
				innerException = ex;
			}
			throw new ArgumentException(SR.GetString("InvalidSerializedData"), innerException);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00066B50 File Offset: 0x00064D50
		public object Deserialize(string inputString)
		{
			return this.Deserialize(inputString, Purpose.User_ObjectStateFormatter_Serialize);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00066B60 File Offset: 0x00064D60
		private object Deserialize(string inputString, Purpose purpose)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				throw new ArgumentNullException("inputString");
			}
			byte[] array = Convert.FromBase64String(inputString);
			int num = array.Length;
			try
			{
				if (AspNetCryptoServiceProvider.Instance.IsDefaultProvider && !this._forceLegacyCryptography)
				{
					if (this._page != null && (this._page.ContainsEncryptedViewState || this._page.EnableViewStateMac))
					{
						Purpose purpose2 = purpose.AppendSpecificPurposes(this.GetSpecificPurposes());
						ICryptoService cryptoService = AspNetCryptoServiceProvider.Instance.GetCryptoService(purpose2, CryptoServiceOptions.None);
						byte[] array2 = cryptoService.Unprotect(array);
						array = array2;
						num = array2.Length;
					}
				}
				else if (this._page != null && this._page.ContainsEncryptedViewState)
				{
					array = MachineKeySection.EncryptOrDecryptData(false, array, this.GetMacKeyModifier(), 0, num);
					num = array.Length;
				}
				else if ((this._page != null && this._page.EnableViewStateMac) || this._macKeyBytes != null)
				{
					array = MachineKeySection.GetDecodedData(array, this.GetMacKeyModifier(), 0, num, ref num);
				}
			}
			catch
			{
				PerfCounters.IncrementCounter(AppPerfCounter.VIEWSTATE_MAC_FAIL);
				ViewStateException.ThrowMacValidationError(null, inputString);
			}
			object result = null;
			MemoryStream memoryStream = ObjectStateFormatter.GetMemoryStream();
			try
			{
				memoryStream.Write(array, 0, num);
				memoryStream.Position = 0L;
				result = this.Deserialize(memoryStream);
			}
			finally
			{
				ObjectStateFormatter.ReleaseMemoryStream(memoryStream);
			}
			return result;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00066CAC File Offset: 0x00064EAC
		private IndexedString DeserializeIndexedString(ObjectStateFormatter.SerializerBinaryReader reader, byte token)
		{
			if (token == 31)
			{
				int num = (int)reader.ReadByte();
				return new IndexedString(this._stringList[num]);
			}
			string s = reader.ReadString();
			this.AddDeserializationStringReference(s);
			return new IndexedString(s);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00066CE8 File Offset: 0x00064EE8
		private Type DeserializeType(ObjectStateFormatter.SerializerBinaryReader reader)
		{
			byte b = reader.ReadByte();
			if (b == 43)
			{
				int index = reader.ReadEncodedInt32();
				return (Type)this._typeList[index];
			}
			string text = reader.ReadString();
			Type type = null;
			try
			{
				if (b == 42)
				{
					type = HttpContext.SystemWebAssembly.GetType(text, true);
				}
				else
				{
					type = Type.GetType(text, true);
				}
			}
			catch (Exception exception)
			{
				if (this._throwOnErrorDeserializing)
				{
					throw;
				}
				WebBaseEvent.RaiseSystemEvent(SR.GetString("Webevent_msg_OSF_Deserialization_Type", new object[]
				{
					text
				}), this, 3011, 0, exception);
			}
			this.AddDeserializationTypeReference(type);
			return type;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00066D8C File Offset: 0x00064F8C
		private object DeserializeValue(ObjectStateFormatter.SerializerBinaryReader reader)
		{
			byte b = reader.ReadByte();
			switch (b)
			{
			case 1:
				return reader.ReadInt16();
			case 2:
				return reader.ReadEncodedInt32();
			case 3:
				return reader.ReadByte();
			case 4:
				return reader.ReadChar();
			case 5:
				return reader.ReadString();
			case 6:
				return DateTime.FromBinary(reader.ReadInt64());
			case 7:
				return reader.ReadDouble();
			case 8:
				return reader.ReadSingle();
			case 9:
				return Color.FromArgb(reader.ReadInt32());
			case 10:
				return Color.FromKnownColor((KnownColor)reader.ReadEncodedInt32());
			case 11:
			{
				Type enumType = this.DeserializeType(reader);
				int value = reader.ReadEncodedInt32();
				return Enum.ToObject(enumType, value);
			}
			case 12:
				return Color.Empty;
			case 13:
			case 14:
			case 17:
			case 18:
			case 19:
			case 26:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 41:
			case 42:
			case 43:
			case 44:
			case 45:
			case 46:
			case 47:
			case 48:
			case 49:
				break;
			case 15:
				return new Pair(this.DeserializeValue(reader), this.DeserializeValue(reader));
			case 16:
				return new Triplet(this.DeserializeValue(reader), this.DeserializeValue(reader), this.DeserializeValue(reader));
			case 20:
			{
				Type elementType = this.DeserializeType(reader);
				int num = reader.ReadEncodedInt32();
				Array array = Array.CreateInstance(elementType, num);
				for (int i = 0; i < num; i++)
				{
					array.SetValue(this.DeserializeValue(reader), i);
				}
				return array;
			}
			case 21:
			{
				int num2 = reader.ReadEncodedInt32();
				string[] array2 = new string[num2];
				for (int j = 0; j < num2; j++)
				{
					array2[j] = reader.ReadString();
				}
				return array2;
			}
			case 22:
			{
				int num3 = reader.ReadEncodedInt32();
				ArrayList arrayList = new ArrayList(num3);
				for (int k = 0; k < num3; k++)
				{
					arrayList.Add(this.DeserializeValue(reader));
				}
				return arrayList;
			}
			case 23:
			case 24:
			{
				int num4 = reader.ReadEncodedInt32();
				IDictionary dictionary;
				if (b == 23)
				{
					dictionary = new Hashtable(num4);
				}
				else
				{
					dictionary = new HybridDictionary(num4);
				}
				for (int l = 0; l < num4; l++)
				{
					dictionary.Add(this.DeserializeValue(reader), this.DeserializeValue(reader));
				}
				return dictionary;
			}
			case 25:
				return this.DeserializeType(reader);
			case 27:
				return new Unit(reader.ReadDouble(), (UnitType)reader.ReadInt32());
			case 28:
				return Unit.Empty;
			case 29:
				return EventValidationStore.DeserializeFrom(reader.BaseStream);
			case 30:
			case 31:
				return this.DeserializeIndexedString(reader, b);
			case 40:
			{
				object result = null;
				Type type = this.DeserializeType(reader);
				string text = reader.ReadString();
				if (type != null)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(type);
					try
					{
						result = converter.ConvertFromInvariantString(text);
					}
					catch (Exception exception)
					{
						if (this._throwOnErrorDeserializing)
						{
							throw;
						}
						WebBaseEvent.RaiseSystemEvent(SR.GetString("Webevent_msg_OSF_Deserialization_String", new object[]
						{
							type.AssemblyQualifiedName
						}), this, 3011, 0, exception);
					}
				}
				return result;
			}
			case 50:
			{
				int num5 = reader.ReadEncodedInt32();
				byte[] buffer = new byte[num5];
				if (num5 != 0)
				{
					reader.Read(buffer, 0, num5);
				}
				object result2 = null;
				MemoryStream memoryStream = ObjectStateFormatter.GetMemoryStream();
				try
				{
					memoryStream.Write(buffer, 0, num5);
					memoryStream.Position = 0L;
					IFormatter formatter = new BinaryFormatter();
					result2 = formatter.Deserialize(memoryStream);
				}
				catch (Exception exception2)
				{
					if (this._throwOnErrorDeserializing)
					{
						throw;
					}
					WebBaseEvent.RaiseSystemEvent(SR.GetString("Webevent_msg_OSF_Deserialization_Binary"), this, 3011, 0, exception2);
				}
				finally
				{
					ObjectStateFormatter.ReleaseMemoryStream(memoryStream);
				}
				return result2;
			}
			default:
				if (b != 60)
				{
					switch (b)
					{
					case 100:
						return null;
					case 101:
						return string.Empty;
					case 102:
						return 0;
					case 103:
						return true;
					case 104:
						return false;
					}
				}
				else
				{
					Type elementType2 = this.DeserializeType(reader);
					int num6 = reader.ReadEncodedInt32();
					int num7 = reader.ReadEncodedInt32();
					if (num7 > num6)
					{
						throw new InvalidOperationException(SR.GetString("InvalidSerializedData"));
					}
					Array array3 = Array.CreateInstance(elementType2, num6);
					for (int m = 0; m < num7; m++)
					{
						int num8 = reader.ReadEncodedInt32();
						if (num8 >= num6 || num8 < 0)
						{
							throw new InvalidOperationException(SR.GetString("InvalidSerializedData"));
						}
						array3.SetValue(this.DeserializeValue(reader), num8);
					}
					return array3;
				}
				break;
			}
			throw new InvalidOperationException(SR.GetString("InvalidSerializedData"));
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x0006725C File Offset: 0x0006545C
		private static MemoryStream GetMemoryStream()
		{
			return new MemoryStream(2048);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00067268 File Offset: 0x00065468
		private void InitializeDeserializer()
		{
			this._typeList = new ArrayList();
			for (int i = 0; i < ObjectStateFormatter.KnownTypes.Length; i++)
			{
				this.AddDeserializationTypeReference(ObjectStateFormatter.KnownTypes[i]);
			}
			this._stringList = new string[255];
			this._stringTableCount = 0;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000672B8 File Offset: 0x000654B8
		private void InitializeSerializer()
		{
			this._typeTable = new HybridDictionary();
			for (int i = 0; i < ObjectStateFormatter.KnownTypes.Length; i++)
			{
				this.AddSerializationTypeReference(ObjectStateFormatter.KnownTypes[i]);
			}
			this._stringList = new string[255];
			this._stringTable = new Hashtable(StringComparer.Ordinal);
			this._stringTableCount = 0;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00067316 File Offset: 0x00065516
		private static void ReleaseMemoryStream(MemoryStream stream)
		{
			stream.Dispose();
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0006731E File Offset: 0x0006551E
		public string Serialize(object stateGraph)
		{
			return this.Serialize(stateGraph, Purpose.User_ObjectStateFormatter_Serialize);
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0006732C File Offset: 0x0006552C
		private string Serialize(object stateGraph, Purpose purpose)
		{
			string result = null;
			MemoryStream memoryStream = ObjectStateFormatter.GetMemoryStream();
			try
			{
				this.Serialize(memoryStream, stateGraph);
				memoryStream.SetLength(memoryStream.Position);
				byte[] array = memoryStream.GetBuffer();
				int length = (int)memoryStream.Length;
				if (AspNetCryptoServiceProvider.Instance.IsDefaultProvider && !this._forceLegacyCryptography)
				{
					if (this._page != null && (this._page.RequiresViewStateEncryptionInternal || this._page.EnableViewStateMac))
					{
						Purpose purpose2 = purpose.AppendSpecificPurposes(this.GetSpecificPurposes());
						ICryptoService cryptoService = AspNetCryptoServiceProvider.Instance.GetCryptoService(purpose2, CryptoServiceOptions.None);
						byte[] array2 = cryptoService.Protect(memoryStream.ToArray());
						array = array2;
						length = array2.Length;
					}
				}
				else if (this._page != null && this._page.RequiresViewStateEncryptionInternal)
				{
					array = MachineKeySection.EncryptOrDecryptData(true, array, this.GetMacKeyModifier(), 0, length);
					length = array.Length;
				}
				else if ((this._page != null && this._page.EnableViewStateMac) || this._macKeyBytes != null)
				{
					array = MachineKeySection.GetEncodedData(array, this.GetMacKeyModifier(), 0, ref length);
				}
				result = Convert.ToBase64String(array, 0, length);
			}
			finally
			{
				ObjectStateFormatter.ReleaseMemoryStream(memoryStream);
			}
			return result;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00067454 File Offset: 0x00065654
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal void SerializeWithAssert(Stream outputStream, object stateGraph)
		{
			this.Serialize(outputStream, stateGraph);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00067460 File Offset: 0x00065660
		public void Serialize(Stream outputStream, object stateGraph)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			this.InitializeSerializer();
			ObjectStateFormatter.SerializerBinaryWriter serializerBinaryWriter = new ObjectStateFormatter.SerializerBinaryWriter(outputStream);
			serializerBinaryWriter.Write(byte.MaxValue);
			serializerBinaryWriter.Write(1);
			this.SerializeValue(serializerBinaryWriter, stateGraph);
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000674A4 File Offset: 0x000656A4
		private void SerializeIndexedString(ObjectStateFormatter.SerializerBinaryWriter writer, string s)
		{
			object obj = this._stringTable[s];
			if (obj != null)
			{
				writer.Write(31);
				writer.Write((byte)((int)obj));
				return;
			}
			this.AddSerializationStringReference(s);
			writer.Write(30);
			writer.Write(s);
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000674F0 File Offset: 0x000656F0
		private void SerializeType(ObjectStateFormatter.SerializerBinaryWriter writer, Type type)
		{
			object obj = this._typeTable[type];
			if (obj != null)
			{
				writer.Write(43);
				writer.WriteEncoded((int)obj);
				return;
			}
			this.AddSerializationTypeReference(type);
			if (type.Assembly == HttpContext.SystemWebAssembly)
			{
				writer.Write(42);
				writer.Write(type.FullName);
				return;
			}
			writer.Write(41);
			writer.Write(type.AssemblyQualifiedName);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00067564 File Offset: 0x00065764
		private void SerializeValue(ObjectStateFormatter.SerializerBinaryWriter writer, object value)
		{
			try
			{
				Stack stack = new Stack();
				stack.Push(value);
				do
				{
					value = stack.Pop();
					if (value == null)
					{
						writer.Write(100);
					}
					else if (value is string)
					{
						string text = (string)value;
						if (text.Length == 0)
						{
							writer.Write(101);
						}
						else
						{
							writer.Write(5);
							writer.Write(text);
						}
					}
					else if (value is int)
					{
						int num = (int)value;
						if (num == 0)
						{
							writer.Write(102);
						}
						else
						{
							writer.Write(2);
							writer.WriteEncoded(num);
						}
					}
					else if (value is Pair)
					{
						writer.Write(15);
						Pair pair = (Pair)value;
						stack.Push(pair.Second);
						stack.Push(pair.First);
					}
					else if (value is Triplet)
					{
						writer.Write(16);
						Triplet triplet = (Triplet)value;
						stack.Push(triplet.Third);
						stack.Push(triplet.Second);
						stack.Push(triplet.First);
					}
					else if (value is IndexedString)
					{
						this.SerializeIndexedString(writer, ((IndexedString)value).Value);
					}
					else if (value.GetType() == typeof(ArrayList))
					{
						writer.Write(22);
						ArrayList arrayList = (ArrayList)value;
						writer.WriteEncoded(arrayList.Count);
						for (int i = arrayList.Count - 1; i >= 0; i--)
						{
							stack.Push(arrayList[i]);
						}
					}
					else if (value is bool)
					{
						if ((bool)value)
						{
							writer.Write(103);
						}
						else
						{
							writer.Write(104);
						}
					}
					else if (value is byte)
					{
						writer.Write(3);
						writer.Write((byte)value);
					}
					else if (value is char)
					{
						writer.Write(4);
						writer.Write((char)value);
					}
					else if (value is DateTime)
					{
						writer.Write(6);
						writer.Write(((DateTime)value).ToBinary());
					}
					else if (value is double)
					{
						writer.Write(7);
						writer.Write((double)value);
					}
					else if (value is short)
					{
						writer.Write(1);
						writer.Write((short)value);
					}
					else if (value is float)
					{
						writer.Write(8);
						writer.Write((float)value);
					}
					else
					{
						if (value is IDictionary)
						{
							bool flag = false;
							if (value.GetType() == typeof(Hashtable))
							{
								writer.Write(23);
								flag = true;
							}
							else if (value.GetType() == typeof(HybridDictionary))
							{
								writer.Write(24);
								flag = true;
							}
							if (flag)
							{
								IDictionary dictionary = (IDictionary)value;
								writer.WriteEncoded(dictionary.Count);
								if (dictionary.Count == 0)
								{
									goto IL_6E4;
								}
								using (IDictionaryEnumerator enumerator = dictionary.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										object obj = enumerator.Current;
										DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
										stack.Push(dictionaryEntry.Value);
										stack.Push(dictionaryEntry.Key);
									}
									goto IL_6E4;
								}
							}
						}
						if (value is EventValidationStore)
						{
							writer.Write(29);
							((EventValidationStore)value).SerializeTo(writer.BaseStream);
						}
						else if (value is Type)
						{
							writer.Write(25);
							this.SerializeType(writer, (Type)value);
						}
						else
						{
							Type type = value.GetType();
							if (value is Array)
							{
								if (((Array)value).Rank <= 1)
								{
									Type elementType = type.GetElementType();
									if (elementType == typeof(string))
									{
										string[] array = (string[])value;
										bool flag2 = false;
										for (int j = 0; j < array.Length; j++)
										{
											if (array[j] == null)
											{
												flag2 = true;
												break;
											}
										}
										if (!flag2)
										{
											writer.Write(21);
											writer.WriteEncoded(array.Length);
											for (int k = 0; k < array.Length; k++)
											{
												writer.Write(array[k]);
											}
											goto IL_6E4;
										}
									}
									Array array2 = (Array)value;
									if (array2.Length > 3)
									{
										int num2 = array2.Length / 4 + 1;
										int num3 = 0;
										List<int> list = new List<int>(num2);
										for (int l = 0; l < array2.Length; l++)
										{
											if (array2.GetValue(l) != null)
											{
												num3++;
												if (num3 >= num2)
												{
													break;
												}
												list.Add(l);
											}
										}
										if (num3 < num2)
										{
											writer.Write(60);
											this.SerializeType(writer, elementType);
											writer.WriteEncoded(array2.Length);
											writer.WriteEncoded(num3);
											using (List<int>.Enumerator enumerator2 = list.GetEnumerator())
											{
												while (enumerator2.MoveNext())
												{
													int num4 = enumerator2.Current;
													writer.WriteEncoded(num4);
													this.SerializeValue(writer, array2.GetValue(num4));
												}
												goto IL_6E4;
											}
										}
									}
									writer.Write(20);
									this.SerializeType(writer, elementType);
									writer.WriteEncoded(array2.Length);
									for (int m = array2.Length - 1; m >= 0; m--)
									{
										stack.Push(array2.GetValue(m));
									}
								}
							}
							else
							{
								if (type.IsEnum)
								{
									Type underlyingType = Enum.GetUnderlyingType(type);
									if (underlyingType == typeof(int))
									{
										writer.Write(11);
										this.SerializeType(writer, type);
										writer.WriteEncoded((int)value);
										goto IL_6E4;
									}
								}
								if (type == typeof(Color))
								{
									Color color = (Color)value;
									if (color.IsEmpty)
									{
										writer.Write(12);
									}
									else if (!color.IsNamedColor)
									{
										writer.Write(9);
										writer.Write(color.ToArgb());
									}
									else
									{
										writer.Write(10);
										writer.WriteEncoded((int)color.ToKnownColor());
									}
								}
								else if (value is Unit)
								{
									Unit unit = (Unit)value;
									if (unit.IsEmpty)
									{
										writer.Write(28);
									}
									else
									{
										writer.Write(27);
										writer.Write(unit.Value);
										writer.Write((int)unit.Type);
									}
								}
								else
								{
									TypeConverter converter = TypeDescriptor.GetConverter(type);
									bool flag3 = Util.CanConvertToFrom(converter, typeof(string));
									if (flag3)
									{
										writer.Write(40);
										this.SerializeType(writer, type);
										writer.Write(converter.ConvertToInvariantString(null, value));
									}
									else
									{
										IFormatter formatter = new BinaryFormatter();
										MemoryStream memoryStream = new MemoryStream(256);
										formatter.Serialize(memoryStream, value);
										byte[] buffer = memoryStream.GetBuffer();
										int num5 = (int)memoryStream.Length;
										writer.Write(50);
										writer.WriteEncoded(num5);
										if (buffer.Length != 0)
										{
											writer.Write(buffer, 0, num5);
										}
									}
								}
							}
						}
					}
					IL_6E4:;
				}
				while (stack.Count > 0);
			}
			catch (Exception ex)
			{
				if (value != null)
				{
					throw new ArgumentException(SR.GetString("ErrorSerializingValue", new object[]
					{
						value.ToString(),
						value.GetType().FullName
					}), ex);
				}
				throw ex;
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00067CE8 File Offset: 0x00065EE8
		object IStateFormatter.Deserialize(string serializedState)
		{
			return this.Deserialize(serializedState);
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00067CF1 File Offset: 0x00065EF1
		string IStateFormatter.Serialize(object state)
		{
			return this.Serialize(state);
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0000298D File Offset: 0x00000B8D
		// (set) Token: 0x0600204B RID: 8267 RVA: 0x00006164 File Offset: 0x00004364
		SerializationBinder IFormatter.Binder
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x00067CFA File Offset: 0x00065EFA
		// (set) Token: 0x0600204D RID: 8269 RVA: 0x00006164 File Offset: 0x00004364
		StreamingContext IFormatter.Context
		{
			get
			{
				return new StreamingContext(StreamingContextStates.All);
			}
			set
			{
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0000298D File Offset: 0x00000B8D
		// (set) Token: 0x0600204F RID: 8271 RVA: 0x00006164 File Offset: 0x00004364
		ISurrogateSelector IFormatter.SurrogateSelector
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00066ACB File Offset: 0x00064CCB
		object IFormatter.Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00067454 File Offset: 0x00065654
		void IFormatter.Serialize(Stream serializationStream, object stateGraph)
		{
			this.Serialize(serializationStream, stateGraph);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00067D06 File Offset: 0x00065F06
		object IStateFormatter2.Deserialize(string serializedState, Purpose purpose)
		{
			return this.Deserialize(serializedState, purpose);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00067D10 File Offset: 0x00065F10
		string IStateFormatter2.Serialize(object state, Purpose purpose)
		{
			return this.Serialize(state, purpose);
		}

		// Token: 0x04001ADC RID: 6876
		private const byte Token_Int16 = 1;

		// Token: 0x04001ADD RID: 6877
		private const byte Token_Int32 = 2;

		// Token: 0x04001ADE RID: 6878
		private const byte Token_Byte = 3;

		// Token: 0x04001ADF RID: 6879
		private const byte Token_Char = 4;

		// Token: 0x04001AE0 RID: 6880
		private const byte Token_String = 5;

		// Token: 0x04001AE1 RID: 6881
		private const byte Token_DateTime = 6;

		// Token: 0x04001AE2 RID: 6882
		private const byte Token_Double = 7;

		// Token: 0x04001AE3 RID: 6883
		private const byte Token_Single = 8;

		// Token: 0x04001AE4 RID: 6884
		private const byte Token_Color = 9;

		// Token: 0x04001AE5 RID: 6885
		private const byte Token_KnownColor = 10;

		// Token: 0x04001AE6 RID: 6886
		private const byte Token_IntEnum = 11;

		// Token: 0x04001AE7 RID: 6887
		private const byte Token_EmptyColor = 12;

		// Token: 0x04001AE8 RID: 6888
		private const byte Token_Pair = 15;

		// Token: 0x04001AE9 RID: 6889
		private const byte Token_Triplet = 16;

		// Token: 0x04001AEA RID: 6890
		private const byte Token_Array = 20;

		// Token: 0x04001AEB RID: 6891
		private const byte Token_StringArray = 21;

		// Token: 0x04001AEC RID: 6892
		private const byte Token_ArrayList = 22;

		// Token: 0x04001AED RID: 6893
		private const byte Token_Hashtable = 23;

		// Token: 0x04001AEE RID: 6894
		private const byte Token_HybridDictionary = 24;

		// Token: 0x04001AEF RID: 6895
		private const byte Token_Type = 25;

		// Token: 0x04001AF0 RID: 6896
		private const byte Token_Unit = 27;

		// Token: 0x04001AF1 RID: 6897
		private const byte Token_EmptyUnit = 28;

		// Token: 0x04001AF2 RID: 6898
		private const byte Token_EventValidationStore = 29;

		// Token: 0x04001AF3 RID: 6899
		private const byte Token_IndexedStringAdd = 30;

		// Token: 0x04001AF4 RID: 6900
		private const byte Token_IndexedString = 31;

		// Token: 0x04001AF5 RID: 6901
		private const byte Token_StringFormatted = 40;

		// Token: 0x04001AF6 RID: 6902
		private const byte Token_TypeRefAdd = 41;

		// Token: 0x04001AF7 RID: 6903
		private const byte Token_TypeRefAddLocal = 42;

		// Token: 0x04001AF8 RID: 6904
		private const byte Token_TypeRef = 43;

		// Token: 0x04001AF9 RID: 6905
		private const byte Token_BinarySerialized = 50;

		// Token: 0x04001AFA RID: 6906
		private const byte Token_SparseArray = 60;

		// Token: 0x04001AFB RID: 6907
		private const byte Token_Null = 100;

		// Token: 0x04001AFC RID: 6908
		private const byte Token_EmptyString = 101;

		// Token: 0x04001AFD RID: 6909
		private const byte Token_ZeroInt32 = 102;

		// Token: 0x04001AFE RID: 6910
		private const byte Token_True = 103;

		// Token: 0x04001AFF RID: 6911
		private const byte Token_False = 104;

		// Token: 0x04001B00 RID: 6912
		private static readonly Type[] KnownTypes = new Type[]
		{
			typeof(object),
			typeof(int),
			typeof(string),
			typeof(bool)
		};

		// Token: 0x04001B01 RID: 6913
		private const byte Marker_Format = 255;

		// Token: 0x04001B02 RID: 6914
		private const byte Marker_Version_1 = 1;

		// Token: 0x04001B03 RID: 6915
		private const int StringTableSize = 255;

		// Token: 0x04001B04 RID: 6916
		private IDictionary _typeTable;

		// Token: 0x04001B05 RID: 6917
		private IDictionary _stringTable;

		// Token: 0x04001B06 RID: 6918
		private IList _typeList;

		// Token: 0x04001B07 RID: 6919
		private int _stringTableCount;

		// Token: 0x04001B08 RID: 6920
		private string[] _stringList;

		// Token: 0x04001B09 RID: 6921
		private byte[] _macKeyBytes;

		// Token: 0x04001B0A RID: 6922
		private readonly bool _forceLegacyCryptography;

		// Token: 0x04001B0B RID: 6923
		private List<string> _specificPurposes;

		// Token: 0x04001B0C RID: 6924
		private bool _throwOnErrorDeserializing;

		// Token: 0x04001B0D RID: 6925
		private Page _page;

		// Token: 0x02000971 RID: 2417
		private sealed class SerializerBinaryReader : BinaryReader
		{
			// Token: 0x06006A0E RID: 27150 RVA: 0x00177AD2 File Offset: 0x00175CD2
			public SerializerBinaryReader(Stream stream) : base(stream)
			{
			}

			// Token: 0x06006A0F RID: 27151 RVA: 0x00177ADB File Offset: 0x00175CDB
			public int ReadEncodedInt32()
			{
				return base.Read7BitEncodedInt();
			}
		}

		// Token: 0x02000972 RID: 2418
		private sealed class SerializerBinaryWriter : BinaryWriter
		{
			// Token: 0x06006A10 RID: 27152 RVA: 0x00177AE3 File Offset: 0x00175CE3
			public SerializerBinaryWriter(Stream stream) : base(stream)
			{
			}

			// Token: 0x06006A11 RID: 27153 RVA: 0x00178E50 File Offset: 0x00177050
			public void WriteEncoded(int value)
			{
				uint num;
				for (num = (uint)value; num >= 128U; num >>= 7)
				{
					this.Write((byte)(num | 128U));
				}
				this.Write((byte)num);
			}
		}
	}
}
