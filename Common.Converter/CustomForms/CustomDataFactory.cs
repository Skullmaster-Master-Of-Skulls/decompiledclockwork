using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;
using TechnoPro.Common.Converter.CustomForms.Converters;
using TechnoPro.Common.Converter.CustomForms.Serializers;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms
{
	// Token: 0x02000002 RID: 2
	public static class CustomDataFactory
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static ICustomDataConverter<T> GetConverter<T>(this CustomDataHolderDTO dataObj) where T : CustomDataHolderDTO
		{
			Type type = dataObj.GetType();
			bool flag = type == typeof(CustomDataBooleanDTO);
			ICustomDataConverter<T> result;
			if (flag)
			{
				result = (ICustomDataConverter<T>)new CustomDataBooleanConverter();
			}
			else
			{
				bool flag2 = type == typeof(CustomDataDateTimeDTO);
				if (flag2)
				{
					result = (ICustomDataConverter<T>)new CustomDataDateTimeConverter();
				}
				else
				{
					bool flag3 = type == typeof(CustomDataFileDTO);
					if (flag3)
					{
						result = (ICustomDataConverter<T>)new CustomDataFileConverter();
					}
					else
					{
						bool flag4 = type == typeof(CustomDataIntDTO);
						if (flag4)
						{
							result = (ICustomDataConverter<T>)new CustomDataIntConverter();
						}
						else
						{
							bool flag5 = type == typeof(CustomDataListItemDTO);
							if (flag5)
							{
								result = (ICustomDataConverter<T>)new CustomDataListItemConverter();
							}
							else
							{
								bool flag6 = type == typeof(CustomDataStringDTO);
								if (flag6)
								{
									result = (ICustomDataConverter<T>)new CustomDataStringConverter();
								}
								else
								{
									bool flag7 = type == typeof(CustomDataBooleanNullableDTO);
									if (!flag7)
									{
										throw new NotImplementedException();
									}
									result = (ICustomDataConverter<T>)new CustomDataBooleanNullableConverter();
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002168 File Offset: 0x00000368
		public static T ConvertData<T>(this CustomDataHolderDTO dataObj) where T : CustomDataHolderDTO
		{
			bool flag = dataObj == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				Type type = dataObj.GetType();
				bool flag2 = type == typeof(CustomDataBooleanDTO);
				if (flag2)
				{
					result = CustomDataFactory.ConvertData<CustomDataBooleanDTO, T>(dataObj as CustomDataBooleanDTO, dataObj.GetConverter<CustomDataBooleanDTO>());
				}
				else
				{
					bool flag3 = type == typeof(CustomDataDateTimeDTO);
					if (flag3)
					{
						result = CustomDataFactory.ConvertData<CustomDataDateTimeDTO, T>(dataObj as CustomDataDateTimeDTO, dataObj.GetConverter<CustomDataDateTimeDTO>());
					}
					else
					{
						bool flag4 = type == typeof(CustomDataFileDTO);
						if (flag4)
						{
							result = CustomDataFactory.ConvertData<CustomDataFileDTO, T>(dataObj as CustomDataFileDTO, dataObj.GetConverter<CustomDataFileDTO>());
						}
						else
						{
							bool flag5 = type == typeof(CustomDataIntDTO);
							if (flag5)
							{
								result = CustomDataFactory.ConvertData<CustomDataIntDTO, T>(dataObj as CustomDataIntDTO, dataObj.GetConverter<CustomDataIntDTO>());
							}
							else
							{
								bool flag6 = type == typeof(CustomDataListItemDTO);
								if (flag6)
								{
									result = CustomDataFactory.ConvertData<CustomDataListItemDTO, T>(dataObj as CustomDataListItemDTO, dataObj.GetConverter<CustomDataListItemDTO>());
								}
								else
								{
									bool flag7 = type == typeof(CustomDataStringDTO);
									if (flag7)
									{
										result = CustomDataFactory.ConvertData<CustomDataStringDTO, T>(dataObj as CustomDataStringDTO, dataObj.GetConverter<CustomDataStringDTO>());
									}
									else
									{
										bool flag8 = type == typeof(CustomDataBooleanNullableDTO);
										if (flag8)
										{
											result = CustomDataFactory.ConvertData<CustomDataBooleanNullableDTO, T>(dataObj as CustomDataBooleanNullableDTO, dataObj.GetConverter<CustomDataBooleanNullableDTO>());
										}
										else
										{
											result = default(T);
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000022D4 File Offset: 0x000004D4
		public static CustomDataHolderDTO ConvertData(this CustomDataHolderDTO dataObj, eCustomDataPrimitiveType controlType)
		{
			CustomDataHolderDTO result;
			switch (controlType)
			{
			case eCustomDataPrimitiveType.String:
				result = dataObj.ConvertData<CustomDataStringDTO>();
				break;
			case eCustomDataPrimitiveType.Int:
				result = dataObj.ConvertData<CustomDataIntDTO>();
				break;
			case eCustomDataPrimitiveType.File:
				result = dataObj.ConvertData<CustomDataFileDTO>();
				break;
			case eCustomDataPrimitiveType.Boolean:
				result = dataObj.ConvertData<CustomDataBooleanDTO>();
				break;
			case eCustomDataPrimitiveType.DateTime:
				result = dataObj.ConvertData<CustomDataDateTimeDTO>();
				break;
			case eCustomDataPrimitiveType.ListItem:
				result = dataObj.ConvertData<CustomDataListItemDTO>();
				break;
			case eCustomDataPrimitiveType.BooleanNullable:
				result = dataObj.ConvertData<CustomDataBooleanNullableDTO>();
				break;
			default:
				throw new ArgumentOutOfRangeException("controlType", controlType, null);
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002360 File Offset: 0x00000560
		private static T ConvertData<F, T>(F dataObj, ICustomDataConverter<F> converter) where F : CustomDataHolderDTO where T : CustomDataHolderDTO
		{
			Type typeFromHandle = typeof(T);
			bool flag = typeFromHandle == typeof(CustomDataBooleanDTO);
			T result;
			if (flag)
			{
				result = (converter.ToCustomDataBoolean(dataObj) as T);
			}
			else
			{
				bool flag2 = typeFromHandle == typeof(CustomDataDateTimeDTO);
				if (flag2)
				{
					result = (converter.ToCustomDataDateTime(dataObj) as T);
				}
				else
				{
					bool flag3 = typeFromHandle == typeof(CustomDataFileDTO);
					if (flag3)
					{
						result = (converter.ToCustomDataFile(dataObj) as T);
					}
					else
					{
						bool flag4 = typeFromHandle == typeof(CustomDataIntDTO);
						if (flag4)
						{
							result = (converter.ToCustomDataInt(dataObj) as T);
						}
						else
						{
							bool flag5 = typeFromHandle == typeof(CustomDataListItemDTO);
							if (flag5)
							{
								result = (converter.ToCustomDataListItem(dataObj) as T);
							}
							else
							{
								bool flag6 = typeFromHandle == typeof(CustomDataStringDTO);
								if (flag6)
								{
									result = (converter.ToCustomDataString(dataObj) as T);
								}
								else
								{
									bool flag7 = typeFromHandle == typeof(CustomDataBooleanNullableDTO);
									if (flag7)
									{
										result = (converter.ToCustomDataBooleanNullable(dataObj) as T);
									}
									else
									{
										result = default(T);
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000024B8 File Offset: 0x000006B8
		public static ICustomDataSerializer<T> GetSerializer<T>(this CustomDataHolder dataObj) where T : CustomDataHolder
		{
			Type type = dataObj.GetType();
			bool flag = type == typeof(CustomDataBoolean);
			ICustomDataSerializer<T> result;
			if (flag)
			{
				result = (ICustomDataSerializer<T>)new CustomDataBooleanSerializer();
			}
			else
			{
				bool flag2 = type == typeof(CustomDataDateTime);
				if (flag2)
				{
					result = (ICustomDataSerializer<T>)new CustomDataDateTimeSerializer();
				}
				else
				{
					bool flag3 = type == typeof(CustomDataFile);
					if (flag3)
					{
						result = (ICustomDataSerializer<T>)new CustomDataFileSerializer();
					}
					else
					{
						bool flag4 = type == typeof(CustomDataInt);
						if (flag4)
						{
							result = (ICustomDataSerializer<T>)new CustomDataIntSerializer();
						}
						else
						{
							bool flag5 = type == typeof(CustomDataListItem);
							if (flag5)
							{
								result = (ICustomDataSerializer<T>)new CustomDataListItemSerializer();
							}
							else
							{
								bool flag6 = type == typeof(CustomDataString);
								if (flag6)
								{
									result = (ICustomDataSerializer<T>)new CustomDataStringSerializer();
								}
								else
								{
									bool flag7 = type == typeof(CustomDataBooleanNullable);
									if (!flag7)
									{
										throw new NotImplementedException();
									}
									result = (ICustomDataSerializer<T>)new CustomDataBooleanNullableSerializer();
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000025D0 File Offset: 0x000007D0
		public static CustomDataSerialized SerializeCustomData(this CustomDataHolder dataObj)
		{
			Type type = dataObj.GetType();
			bool flag = type == typeof(CustomDataBoolean);
			CustomDataSerialized result;
			if (flag)
			{
				result = CustomDataFactory.SerializeCustomData<CustomDataBoolean>(new CustomDataBooleanSerializer(), dataObj as CustomDataBoolean);
			}
			else
			{
				bool flag2 = type == typeof(CustomDataDateTime);
				if (flag2)
				{
					result = CustomDataFactory.SerializeCustomData<CustomDataDateTime>(new CustomDataDateTimeSerializer(), dataObj as CustomDataDateTime);
				}
				else
				{
					bool flag3 = type == typeof(CustomDataFile);
					if (flag3)
					{
						result = CustomDataFactory.SerializeCustomData<CustomDataFile>(new CustomDataFileSerializer(), dataObj as CustomDataFile);
					}
					else
					{
						bool flag4 = type == typeof(CustomDataInt);
						if (flag4)
						{
							result = CustomDataFactory.SerializeCustomData<CustomDataInt>(new CustomDataIntSerializer(), dataObj as CustomDataInt);
						}
						else
						{
							bool flag5 = type == typeof(CustomDataListItem);
							if (flag5)
							{
								result = CustomDataFactory.SerializeCustomData<CustomDataListItem>(new CustomDataListItemSerializer(), dataObj as CustomDataListItem);
							}
							else
							{
								bool flag6 = type == typeof(CustomDataString);
								if (flag6)
								{
									result = CustomDataFactory.SerializeCustomData<CustomDataString>(new CustomDataStringSerializer(), dataObj as CustomDataString);
								}
								else
								{
									bool flag7 = type == typeof(CustomDataBooleanNullable);
									if (flag7)
									{
										result = CustomDataFactory.SerializeCustomData<CustomDataBooleanNullable>(new CustomDataBooleanNullableSerializer(), dataObj as CustomDataBooleanNullable);
									}
									else
									{
										result = null;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002710 File Offset: 0x00000910
		public static CustomDataSerialized SerializeCustomData<T>(ICustomDataSerializer<T> serializer, T dataObj) where T : CustomDataHolder
		{
			return serializer.Serialize(dataObj);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000272C File Offset: 0x0000092C
		public static CustomDataHolder GetCustomData(this CustomDataSerialized serializedData)
		{
			eCustomDataPrimitiveType dataPrimitiveType = serializedData.DataPrimitiveType;
			Guid dataInstanceId = serializedData.DataInstanceId;
			CustomDataHolder result;
			switch (dataPrimitiveType)
			{
			case eCustomDataPrimitiveType.String:
				result = new CustomDataString(dataInstanceId, dataPrimitiveType).GetSerializer<CustomDataString>().DeSerialize(serializedData);
				break;
			case eCustomDataPrimitiveType.Int:
				result = new CustomDataInt(dataInstanceId, dataPrimitiveType).GetSerializer<CustomDataInt>().DeSerialize(serializedData);
				break;
			case eCustomDataPrimitiveType.File:
				result = new CustomDataFile(dataInstanceId, dataPrimitiveType).GetSerializer<CustomDataFile>().DeSerialize(serializedData);
				break;
			case eCustomDataPrimitiveType.Boolean:
				result = new CustomDataBoolean(dataInstanceId, dataPrimitiveType).GetSerializer<CustomDataBoolean>().DeSerialize(serializedData);
				break;
			case eCustomDataPrimitiveType.DateTime:
				result = new CustomDataDateTime(dataInstanceId, dataPrimitiveType).GetSerializer<CustomDataDateTime>().DeSerialize(serializedData);
				break;
			case eCustomDataPrimitiveType.ListItem:
				result = new CustomDataListItem(dataInstanceId, dataPrimitiveType).GetSerializer<CustomDataListItem>().DeSerialize(serializedData);
				break;
			case eCustomDataPrimitiveType.BooleanNullable:
				result = new CustomDataBooleanNullable(dataInstanceId, dataPrimitiveType).GetSerializer<CustomDataBooleanNullable>().DeSerialize(serializedData);
				break;
			default:
				throw new InvalidOperationException();
			}
			return result;
		}
	}
}
