using System.Collections.Generic;

namespace Lga.Id.Infrastructure.Services.BulkUpload
{
    public class RowsValidationEntity
    {
        int? _totalNumberofDataRows;
        int? _TotalNumberOfDataRowsWithErrors;
        List<RowValidationEntity> _rowsValidationInfo;

        public RowsValidationEntity()
        {
            _totalNumberofDataRows = null;
            _TotalNumberOfDataRowsWithErrors = null;
            _rowsValidationInfo = new List<RowValidationEntity>();
        }

        public int? TotalNumberofDataRows
        {
            get { return _totalNumberofDataRows; }
            set { _totalNumberofDataRows = value; }
        }

        public int? TotalNumberOfRowsWithErrors
        {
            get { return _TotalNumberOfDataRowsWithErrors; }
            set { _TotalNumberOfDataRowsWithErrors = value; }
        }

        public List<RowValidationEntity> RowsValidationInfo
        {
            get { return _rowsValidationInfo; }
            set
            { _rowsValidationInfo = value; }
        }

        public string UploadType { get; set; }

        public string FileName { get; set; }


    }
}
