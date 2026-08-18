using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000149 RID: 329
	public static class UpdateFileTypeFactory
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x000110BC File Offset: 0x0000F2BC
		public static IUpdateFileType GetUpdateFileType(string fileType)
		{
			bool flag = string.IsNullOrEmpty(fileType);
			IUpdateFileType result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = fileType.Equals(eUpdateFileTypes.Database_patch.GetTitle());
				if (flag2)
				{
					result = new DatabasePatchFileType();
				}
				else
				{
					bool flag3 = fileType.Equals(eUpdateFileTypes.Database_files_patch.GetTitle());
					if (flag3)
					{
						result = new DatabaseFilesPatchFileType();
					}
					else
					{
						bool flag4 = fileType.Equals(eUpdateFileTypes.Database_tracking_patch.GetTitle());
						if (flag4)
						{
							result = new DatabaseTrackingPatchFileType();
						}
						else
						{
							bool flag5 = fileType.Equals(eUpdateFileTypes.ClockWorkServer_update.GetTitle());
							if (flag5)
							{
								result = new ClockWorkServerUpdateFileType();
							}
							else
							{
								bool flag6 = fileType.Equals(eUpdateFileTypes.ClockWorkWeb_update.GetTitle());
								if (flag6)
								{
									result = new ClockWorkWebUpdateFileType();
								}
								else
								{
									bool flag7 = fileType.Equals(eUpdateFileTypes.ClockWork_update.GetTitle());
									if (flag7)
									{
										result = new ClockWorkUpdateFileType();
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

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001117C File Offset: 0x0000F37C
		public static IUpdateFileType GetUpdateFileType(eUpdateFileTypes fileType)
		{
			IUpdateFileType result;
			switch (fileType)
			{
			case eUpdateFileTypes.Database_patch:
				result = new DatabasePatchFileType();
				break;
			case eUpdateFileTypes.Database_files_patch:
				result = new DatabaseFilesPatchFileType();
				break;
			case eUpdateFileTypes.Database_tracking_patch:
				result = new DatabaseTrackingPatchFileType();
				break;
			case eUpdateFileTypes.ClockWorkServer_update:
				result = new ClockWorkServerUpdateFileType();
				break;
			case eUpdateFileTypes.ClockWorkWeb_update:
				result = new ClockWorkWebUpdateFileType();
				break;
			case eUpdateFileTypes.ClockWork_update:
				result = new ClockWorkUpdateFileType();
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000111E4 File Offset: 0x0000F3E4
		public static IList<FileType> GetFileTypes()
		{
			return new List<FileType>
			{
				new FileType
				{
					Title = eUpdateFileTypes.Database_patch.GetTitle(),
					Extension = eUpdateFileTypes.Database_patch.GetExtension(),
					AddrSizeVersion = eUpdateFileTypes.Database_patch.GetAddSizeVersion(),
					Description = eUpdateFileTypes.Database_patch.GetDescription()
				},
				new FileType
				{
					Title = eUpdateFileTypes.Database_files_patch.GetTitle(),
					Extension = eUpdateFileTypes.Database_files_patch.GetExtension(),
					AddrSizeVersion = eUpdateFileTypes.Database_files_patch.GetAddSizeVersion(),
					Description = eUpdateFileTypes.Database_files_patch.GetDescription()
				},
				new FileType
				{
					Title = eUpdateFileTypes.Database_tracking_patch.GetTitle(),
					Extension = eUpdateFileTypes.Database_tracking_patch.GetExtension(),
					AddrSizeVersion = eUpdateFileTypes.Database_tracking_patch.GetAddSizeVersion(),
					Description = eUpdateFileTypes.Database_tracking_patch.GetDescription()
				},
				new FileType
				{
					Title = eUpdateFileTypes.ClockWorkServer_update.GetTitle(),
					Extension = eUpdateFileTypes.ClockWorkServer_update.GetExtension(),
					AddrSizeVersion = eUpdateFileTypes.ClockWorkServer_update.GetAddSizeVersion(),
					Description = eUpdateFileTypes.ClockWorkServer_update.GetDescription()
				},
				new FileType
				{
					Title = eUpdateFileTypes.ClockWorkWeb_update.GetTitle(),
					Extension = eUpdateFileTypes.ClockWorkWeb_update.GetExtension(),
					AddrSizeVersion = eUpdateFileTypes.ClockWorkWeb_update.GetAddSizeVersion(),
					Description = eUpdateFileTypes.ClockWorkWeb_update.GetDescription()
				},
				new FileType
				{
					Title = eUpdateFileTypes.ClockWork_update.GetTitle(),
					Extension = eUpdateFileTypes.ClockWork_update.GetExtension(),
					AddrSizeVersion = eUpdateFileTypes.ClockWork_update.GetAddSizeVersion(),
					Description = eUpdateFileTypes.ClockWork_update.GetDescription()
				}
			};
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001137C File Offset: 0x0000F57C
		public static FileType GetFileType(string fileTypeTitle)
		{
			eUpdateFileTypes[] source = (eUpdateFileTypes[])Enum.GetValues(typeof(eUpdateFileTypes));
			List<eUpdateFileTypes> list = (from ft in source
			where ft.GetTitle() == fileTypeTitle
			select ft).ToList<eUpdateFileTypes>();
			FileType result;
			if (list.Count <= 0)
			{
				result = null;
			}
			else
			{
				FileType fileType = new FileType();
				fileType.Title = list.First<eUpdateFileTypes>().GetTitle();
				fileType.AddrSizeVersion = list.First<eUpdateFileTypes>().GetAddSizeVersion();
				fileType.Extension = list.First<eUpdateFileTypes>().GetExtension();
				result = fileType;
				fileType.Description = list.First<eUpdateFileTypes>().GetDescription();
			}
			return result;
		}
	}
}
