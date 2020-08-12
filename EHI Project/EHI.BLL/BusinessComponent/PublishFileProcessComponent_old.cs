using IFG.Automation.BLL.IBusinessComponent;
using IFG.Automation.BLL.Utility;
using IFG.Automation.DAL.IDataAccessRepository;
using IFG.Automation.Models;
using IFG.Automation.Models.Models;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IFG.Automation.BLL.BusinessComponent
{
    public class PublishFileProcessComponent_old : IPublishFileProcessComponent
    {
        private readonly AppSettings _appSettings;
        private string _importFilePath = string.Empty;
        private string _importErrorFilePath = string.Empty;
        string _mSTemplateName = string.Empty;
        string _importStatusPending = string.Empty;
        string _importStatusSuccess = string.Empty;
        string _importStatusInprogress = string.Empty;
        string _importStatusFailed = string.Empty;
        string _adminUserId = string.Empty;
        string _loggerFilePath = string.Empty;
        string _ImportProcessedFilePath = string.Empty;
        int _BatchSize = 0;
        private readonly IPublishDataRepository _publishDataRepository;
        private readonly IPublishStatusRepository _publishStatusRepository;
        private readonly PublishDImport _publishDImport;
        public PublishFileProcessComponent_old(IOptions<AppSettings> appSettings, IPublishDataRepository publishDataRepository, IPublishStatusRepository publishStatusRepository
            , IOptions<PublishDImport> publishDImport)
        {
            _appSettings = appSettings.Value;
            _publishDataRepository = publishDataRepository;
            _publishStatusRepository = publishStatusRepository;
            _publishDImport = publishDImport.Value;
        }

        public async Task<PublishResponseModel> ProcessFile()
        {
            FileLogger.WriteToFile(_appSettings.LoggerFilePath, "Start ProcessMSImport At: " + DateTime.Now);
            IEnumerable<PublishDataProcessModel> pendingRequest = await _publishDataRepository.GetPublishDataProcess();
            //iImportRequestService.GetImportRequest(_mSTemplateName, _importStatusPending);

            if (pendingRequest != null && pendingRequest.Count() > 0)
            {
                foreach (var item in pendingRequest)
                {
                    var res = await Import(item);
                    res.ImportFor = _appSettings.PDImportName;

                    FileOperations.Move(item.PublishFileName, item.PublishFilePath, _appSettings.ImportProcessedFilePath);

                    StringBuilder sbMessage = new StringBuilder();
                    sbMessage.Append("Excuted On");
                    sbMessage.Append(DateTime.Now.ToString());
                    sbMessage.Append("_ProcessMSImport()_");
                    sbMessage.Append(res.ImportFor);
                    sbMessage.Append("_TotalRecords:");
                    sbMessage.Append(res.TotalRecords);
                    sbMessage.Append("_");
                    sbMessage.Append(res.ResponseStatus);
                    sbMessage.Append("_");
                    sbMessage.Append(res.ResponseMessage);
                    sbMessage.Append(Environment.NewLine);

                    FileLogger.WriteToFile(_loggerFilePath, sbMessage.ToString());
                }
            }
            else
            {
                StringBuilder sbMessage = new StringBuilder();
                sbMessage.Append("Excuted On");
                sbMessage.Append(DateTime.Now.ToString());
                sbMessage.Append("_ProcessMSImport()_");
                sbMessage.Append(" No record(s) found to process");
                sbMessage.Append(Environment.NewLine);

                FileLogger.WriteToFile(_loggerFilePath, sbMessage.ToString());
            }

            FileLogger.WriteToFile(_loggerFilePath, "End ProcessMSImport At: " + DateTime.Now);

            return await Task.FromResult<PublishResponseModel>(new PublishResponseModel());

        }


        private async Task<ImportResponse> Import(PublishDataProcessModel requestModel)
        {
            //var filePath = _importFilePath + "Market Size Import.xlsx"; 
            var res = new ImportResponse();
            List<ExcelUploadModel> objExcelUploadList = new List<ExcelUploadModel>();
            //var inputModel = new MarketSizingImportInsertViewModel();
            //IDbHelper _IDbHelper = new DbHelper(configuration);
            //IImportRequestRepository iImportRequestRepository = new ImportRequestRepository(_IDbHelper);
            //IImportRequestService iImportRequestService = new ImportRequestService(iImportRequestRepository);
            try
            {

                //inputModel.TemplateName = _mSTemplateName;

                //------------Call business method

                //  IMarketSizingImportExportRepository imarketSizingImportExportRepository = new MarketSizingImportExportRepository(_IDbHelper);
                //  IMarketSizingImportExportService imarketSizingImportExportService = new MarketSizingImportExportService(imarketSizingImportExportRepository);
                //  inputModel.UserCreatedById = Guid.Parse(_adminUserId);
                var resultStag = false;
                var rowCount = 0;

                //------------Change Status to InProgress--------------------------
                await UpdatePublishDataStatus(requestModel.Id, _appSettings.ImportStatusInProgress);

                //ImportRequestViewModel objImportRequestViewModel = new ImportRequestViewModel();
                //objImportRequestViewModel.Id = requestModel.Id;
                //objImportRequestViewModel.ImportException = "Market sizing importe is processing.";
                //objImportRequestViewModel.StatusName = _importStatusInprogress;
                //iImportRequestService.Update(objImportRequestViewModel);
                //--------------------------------------

                //check file exist

                using (var stream = File.OpenRead(requestModel.PublishFilePath))
                {
                    //formFile.CopyTo(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                        if (worksheet.Dimension == null)
                        {
                            res.ResponseMessage = "Please upload proper excel file";
                            res.TotalSuccessRecords = 0;
                            res.TotalFailedRecords = 0;
                            res.ResponseStatus = "Failed";
                            return res;
                        }

                        rowCount = worksheet.Dimension.Rows;
                        var rowFrom = 2;
                        for (int row = rowFrom; row <= rowCount; row++)
                        {
                            var objMarketSizingImport = ExcelCellToModel(worksheet, row);
                            objMarketSizingImport.WebsiteURL = requestModel.WebsiteURL;
                            objMarketSizingImport.UserName = requestModel.UserName;
                            objMarketSizingImport.Password = requestModel.Password;
                            objExcelUploadList.Add(objMarketSizingImport);
                        }
                    }
                }

                Parallel.ForEach(objExcelUploadList, item =>
                {
                    //string postRes = PublishApiUtility.PostData(item);
                    //item.PublishResponse = postRes;
                });

                //}              

                res.TotalRecords = rowCount;
                //var resError = result != null ? result.Where(s => s.ErrorNotes != null).ToList() : null;
                //if (resError != null && resError.Count <= 0 || resError.First().ErrorNotes == null)
                //{
                //    await UpdatePublishDataStatus(requestModel.Id, _appSettings.ImportStatusSuccess);
                //}
                //else
                //{
                //    var errorFilePath = string.Empty;
                //    if (result != null && result.Count > 0)
                //    {
                //        errorFilePath = CreateMSExcel(result);
                //    }  
                //    await UpdatePublishDataStatus(requestModel.Id, _appSettings.ImportStatusFailed);
                //}
            }
            catch (Exception ex)
            {
                await UpdatePublishDataStatus(requestModel.Id, _appSettings.ImportStatusFailed);
                //_logger.LogError(ex.Message);
                //res.ResponseMessage = "Import() " + ex.Message;
                //res.ResponseStatus = _importStatusFailed;

                //var objImportRequestViewModel = new ImportRequestViewModel();
                //objImportRequestViewModel.Id = requestModel.Id;
                //objImportRequestViewModel.StatusName = _importStatusFailed;
                //objImportRequestViewModel.ImportException = "Import marketSizing have some errors";
                //objImportRequestViewModel.ImportExceptionFilePath = string.Empty;
                //iImportRequestService.Update(objImportRequestViewModel);

            }
            return res;
        }

        private async Task UpdatePublishDataStatus(Guid publishDataId, string statusName )
        {
           
            PublishStatusViewModel statusModel = new PublishStatusViewModel();
            statusModel = await _publishStatusRepository.GetPublishStatus(statusName);
            PublishDataViewModel viewModel = new PublishDataViewModel();
            viewModel.PublishStatusId = statusModel.Id;
            viewModel.Id = publishDataId;
            await _publishDataRepository.UpdatPublishDataStatus(viewModel);
        }

        private ExcelUploadModel ExcelCellToModel(ExcelWorksheet worksheet, int row)
        {            
            ExcelUploadModel msdata = new ExcelUploadModel
            {
                SrNo = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                Title = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                Content = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                FeaturedImage = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                Category = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                Tags = worksheet.Cells[row, 6].Value?.ToString().Trim()
            };
            return msdata;
        }

        public string CreateMSExcel(List<ExcelUploadModel> model)
        {

            string sFileName = @"PublishDataImportResponse" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xlsx";
           
            var fullPath = Path.Combine(_appSettings.ImportErrorFilePath, sFileName);
            FileInfo file = new FileInfo(fullPath);
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(_appSettings.ImportErrorFilePath, sFileName));
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("PublishDataErrorSheet");
                //First add the headers
                worksheet.Cells["A1"].Value = "PublishData";
                worksheet.Cells["B1"].Value = "RowNo";
                worksheet.Cells["C1"].Value = "ErrorNotes";
                //Add values
                int row = 2;
                for (int i = 0; i < model.Count; i++)
                {                  
                    //worksheet.Cells["A" + row].Value = model[i].Market;
                    //worksheet.Cells["B" + row].Value = model[i].RowNo.ToString();
                    //worksheet.Cells["C" + row].Value = model[i].ErrorNotes;
                    row++;
                }    
                package.Save(); //Save the workbook.
            }
            return fullPath;
        }


    }
}
