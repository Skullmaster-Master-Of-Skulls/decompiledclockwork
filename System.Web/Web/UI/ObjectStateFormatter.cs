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
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x02000432 RID: 1074
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ObjectStateFormatter : IStateFormatter, IFormatter
	{
		// Token: 0x06003376 RID: 13174 RVA: 0x000DF73D File Offset: 0x000DE73D
		public ObjectStateFormatter() : this(null)
		{
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000DF746 File Offset: 0x000DE746
		internal ObjectStateFormatter(byte[] macEncodingKey) : this(null, true)
		{
			this._macKeyBytes = macEncodingKey;
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000DF757 File Offset: 0x000DE757
		internal ObjectStateFormatter(Page page, bool throwOnErrorDeserializing)
		{
			this._page = page;
			this._throwOnErrorDeserializing = throwOnErrorDeserializing;
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000DF770 File Offset: 0x000DE770
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

		// Token: 0x0600337A RID: 13178 RVA: 0x000DF825 File Offset: 0x000DE825
		private void AddDeserializationStringReference(string s)
		{
			if (this._stringTableCount == 255)
			{
				this._stringTableCount = 0;
			}
			this._stringList[this._stringTableCount] = s;
			this._stringTableCount++;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000DF857 File Offset: 0x000DE857
		private void AddDeserializationTypeReference(Type type)
		{
			this._typeList.Add(type);
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000DF868 File Offset: 0x000DE868
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

		// Token: 0x0600337D RID: 13181 RVA: 0x000DF8DC File Offset: 0x000DE8DC
		private void AddSerializationTypeReference(Type type)
		{
			int count = this._typeTable.Count;
			this._typeTable[type] = count;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000DF907 File Offset: 0x000DE907
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal object DeserializeWithAssert(Stream inputStream)
		{
			return this.Deserialize(inputStream);
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000DF910 File Offset: 0x000DE910
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

		// Token: 0x06003380 RID: 13184 RVA: 0x000DF98C File Offset: 0x000DE98C
		public object Deserialize(string inputString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				throw new ArgumentNullException("inputString");
			}
			byte[] array = Convert.FromBase64String(inputString);
			int num = array.Length;
			try
			{
				if (this._page != null && this._page.ContainsEncryptedViewState)
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

		// Token: 0x06003381 RID: 13185 RVA: 0x000DFA6C File Offset: 0x000DEA6C
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

		// Token: 0x06003382 RID: 13186 RVA: 0x000DFAA8 File Offset: 0x000DEAA8
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

		// Token: 0x06003383 RID: 13187 RVA: 0x000DFB50 File Offset: 0x000DEB50
		private object DeserializeValue(ObjectStateFormatter.SerializerBinaryReader reader)
		{
			byte b = reader.ReadByte();
			byte b2 = b;
			switch (b2)
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
			case 29:
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
				if (b2 != 60)
				{
					switch (b2)
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

		// Token: 0x06003384 RID: 13188 RVA: 0x000E001C File Offset: 0x000DF01C
		private static MemoryStream GetMemoryStream()
		{
			MemoryStream memoryStream = null;
			if (ObjectStateFormatter._streams.Count > 0)
			{
				lock (ObjectStateFormatter._streams)
				{
					if (ObjectStateFormatter._streams.Count > 0)
					{
						memoryStream = (MemoryStream)ObjectStateFormatter._streams.Pop();
					}
				}
			}
			if (memoryStream == null)
			{
				memoryStream = new MemoryStream(2048);
			}
			return memoryStream;
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000E008C File Offset: 0x000DF08C
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

		// Token: 0x06003386 RID: 13190 RVA: 0x000E00DC File Offset: 0x000DF0DC
		private void InitializeSerializer()
		{
			this._typeTable = new HybridDictionary();
			for (int i = 0; i < ObjectStateFormatter.KnownTypes.Length; i++)
			{
				this.AddSerializationTypeReference(ObjectStateFormatter.KnownTypes[i]);
			}
			this._stringList = new string[255];
			this._stringTable = new Hashtable();
			this._stringTableCount = 0;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000E0138 File Offset: 0x000DF138
		private static void ReleaseMemoryStream(MemoryStream stream)
		{
			stream.Position = 0L;
			stream.SetLength(0L);
			lock (ObjectStateFormatter._streams)
			{
				ObjectStateFormatter._streams.Push(stream);
			}
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000E0188 File Offset: 0x000DF188
		public string Serialize(object stateGraph)
		{
			string result = null;
			MemoryStream memoryStream = ObjectStateFormatter.GetMemoryStream();
			try
			{
				this.Serialize(memoryStream, stateGraph);
				memoryStream.SetLength(memoryStream.Position);
				byte[] array = memoryStream.GetBuffer();
				int length = (int)memoryStream.Length;
				if (this._page != null && this._page.RequiresViewStateEncryptionInternal)
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

		// Token: 0x06003389 RID: 13193 RVA: 0x000E023C File Offset: 0x000DF23C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal void SerializeWithAssert(Stream outputStream, object stateGraph)
		{
			this.Serialize(outputStream, stateGraph);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000E0248 File Offset: 0x000DF248
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

		// Token: 0x0600338B RID: 13195 RVA: 0x000E028C File Offset: 0x000DF28C
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

		// Token: 0x0600338C RID: 13196 RVA: 0x000E02D8 File Offset: 0x000DF2D8
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

		// Token: 0x0600338D RID: 13197 RVA: 0x000E0348 File Offset: 0x000DF348
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
									goto IL_6A5;
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
									goto IL_6A5;
								}
							}
						}
						if (value is Type)
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
											goto IL_6A5;
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
												goto IL_6A5;
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
										goto IL_6A5;
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
					IL_6A5:;
				}
				while (stack.Count > 0);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException(SR.GetString("ErrorSerializingValue", new object[]
				{
					value.ToString(),
					value.GetType().FullName
				}), innerException);
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000E0A8C File Offset: 0x000DFA8C
		object IStateFormatter.Deserialize(string serializedState)
		{
			return this.Deserialize(serializedState);
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000E0A95 File Offset: 0x000DFA95
		string IStateFormatter.Serialize(object state)
		{
			return this.Serialize(state);
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x000E0A9E File Offset: 0x000DFA9E
		// (set) Token: 0x06003391 RID: 13201 RVA: 0x000E0AA1 File Offset: 0x000DFAA1
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

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000E0AA3 File Offset: 0x000DFAA3
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x000E0AAF File Offset: 0x000DFAAF
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

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000E0AB1 File Offset: 0x000DFAB1
		// (set) Token: 0x06003395 RID: 13205 RVA: 0x000E0AB4 File Offset: 0x000DFAB4
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

		// Token: 0x06003396 RID: 13206 RVA: 0x000E0AB6 File Offset: 0x000DFAB6
		object IFormatter.Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000E0ABF File Offset: 0x000DFABF
		void IFormatter.Serialize(Stream serializationStream, object stateGraph)
		{
			this.Serialize(serializationStream, stateGraph);
		}

		// Token: 0x04002407 RID: 9223
		private const byte Token_Int16 = 1;

		// Token: 0x04002408 RID: 9224
		private const byte Token_Int32 = 2;

		// Token: 0x04002409 RID: 9225
		private const byte Token_Byte = 3;

		// Token: 0x0400240A RID: 9226
		private const byte Token_Char = 4;

		// Token: 0x0400240B RID: 9227
		private const byte Token_String = 5;

		// Token: 0x0400240C RID: 9228
		private const byte Token_DateTime = 6;

		// Token: 0x0400240D RID: 9229
		private const byte Token_Double = 7;

		// Token: 0x0400240E RID: 9230
		private const byte Token_Single = 8;

		// Token: 0x0400240F RID: 9231
		private const byte Token_Color = 9;

		// Token: 0x04002410 RID: 9232
		private const byte Token_KnownColor = 10;

		// Token: 0x04002411 RID: 9233
		private const byte Token_IntEnum = 11;

		// Token: 0x04002412 RID: 9234
		private const byte Token_EmptyColor = 12;

		// Token: 0x04002413 RID: 9235
		private const byte Token_Pair = 15;

		// Token: 0x04002414 RID: 9236
		private const byte Token_Triplet = 16;

		// Token: 0x04002415 RID: 9237
		private const byte Token_Array = 20;

		// Token: 0x04002416 RID: 9238
		private const byte Token_StringArray = 21;

		// Token: 0x04002417 RID: 9239
		private const byte Token_ArrayList = 22;

		// Token: 0x04002418 RID: 9240
		private const byte Token_Hashtable = 23;

		// Token: 0x04002419 RID: 9241
		private const byte Token_HybridDictionary = 24;

		// Token: 0x0400241A RID: 9242
		private const byte Token_Type = 25;

		// Token: 0x0400241B RID: 9243
		private const byte Token_Unit = 27;

		// Token: 0x0400241C RID: 9244
		private const byte Token_EmptyUnit = 28;

		// Token: 0x0400241D RID: 9245
		private const byte Token_IndexedStringAdd = 30;

		// Token: 0x0400241E RID: 9246
		private const byte Token_IndexedString = 31;

		// Token: 0x0400241F RID: 9247
		private const byte Token_StringFormatted = 40;

		// Token: 0x04002420 RID: 9248
		private const byte Token_TypeRefAdd = 41;

		// Token: 0x04002421 RID: 9249
		private const byte Token_TypeRefAddLocal = 42;

		// Token: 0x04002422 RID: 9250
		private const byte Token_TypeRef = 43;

		// Token: 0x04002423 RID: 9251
		private const byte Token_BinarySerialized = 50;

		// Token: 0x04002424 RID: 9252
		private const byte Token_SparseArray = 60;

		// Token: 0x04002425 RID: 9253
		private const byte Token_Null = 100;

		// Token: 0x04002426 RID: 9254
		private const byte Token_EmptyString = 101;

		// Token: 0x04002427 RID: 9255
		private const byte Token_ZeroInt32 = 102;

		// Token: 0x04002428 RID: 9256
		private const byte Token_True = 103;

		// Token: 0x04002429 RID: 9257
		private const byte Token_False = 104;

		// Token: 0x0400242A RID: 9258
		private const byte Marker_Format = 255;

		// Token: 0x0400242B RID: 9259
		private const byte Marker_Version_1 = 1;

		// Token: 0x0400242C RID: 9260
		private const int StringTableSize = 255;

		// Token: 0x0400242D RID: 9261
		private static readonly Type[] KnownTypes = new Type[]
		{
			typeof(object),
			typeof(int),
			typeof(string),
			typeof(bool)
		};

		// Token: 0x0400242E RID: 9262
		private static readonly Stack _streams = new Stack();

		// Token: 0x0400242F RID: 9263
		private IDictionary _typeTable;

		// Token: 0x04002430 RID: 9264
		private IDictionary _stringTable;

		// Token: 0x04002431 RID: 9265
		private IList _typeList;

		// Token: 0x04002432 RID: 9266
		private int _stringTableCount;

		// Token: 0x04002433 RID: 9267
		private string[] _stringList;

		// Token: 0x04002434 RID: 9268
		private byte[] _macKeyBytes;

		// Token: 0x04002435 RID: 9269
		private bool _throwOnErrorDeserializing;

		// Token: 0x04002436 RID: 9270
		private Page _page;

		// Token: 0x02000433 RID: 1075
		private sealed class SerializerBinaryReader : BinaryReader
		{
			// Token: 0x06003399 RID: 13209 RVA: 0x000E0B24 File Offset: 0x000DFB24
			public SerializerBinaryReader(Stream stream) : base(stream)
			{
			}

			// Token: 0x0600339A RID: 13210 RVA: 0x000E0B2D File Offset: 0x000DFB2D
			public int ReadEncodedInt32()
			{
				return base.Read7BitEncodedInt();
			}
		}

		// Token: 0x02000434 RID: 1076
		private sealed class SerializerBinaryWriter : BinaryWriter
		{
			// Token: 0x0600339B RID: 13211 RVA: 0x000E0B35 File Offset: 0x000DFB35
			public SerializerBinaryWriter(Stream stream) : base(stream)
			{
			}

			// Token: 0x0600339C RID: 13212 RVA: 0x000E0B40 File Offset: 0x000DFB40
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
