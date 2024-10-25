using System.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;

namespace Karyon.Models
{
    public class EventList : Menu
    {        public List<EventListData>? EventListDetails { get; set; }
    }

    public class EventListRequest : Menu
    {
        public string? Pk_EventId { get; set; }
        public string? Image { get; set; }
        public IFormFile? Imagefile { get; set; }
        public string? Pk_EventBookingId { get; set; }
        public DataSet GetEventList()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_MemId", Fk_MemId),
                new SqlParameter("@OpCode", OpCode)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.EventManage, para);
            return ds;
        }
        public DataSet UploadAssociateImage()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_BookingId", Pk_EventBookingId),
                new SqlParameter("@AssociateImage", Image),
                new SqlParameter("@OpCode", 6)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.EventManage, para);
            return ds;
        }
    }
    public class EventListData : Menu
    {
        public string? Fk_MemId { get; set; }
        public string? Pk_EventId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
        public string? EventImage { get; set; }
        public bool? IsConfirmed { get; set; }
        public string? EventTime { get; set; }
        public string? Address { get; set; }
        public string? AvaliableSeats { get; set; }
        public string? QrCode { get; set; }
        public string? NoOfSeats { get; set; }
        public string? Amount { get; set; }
        public string? PV { get; set; }
        public string? AssociateImage { get; set; }
        public string? Pk_EventBookingId { get; set; }
        public string? ProfilePic { get; set; }
        public string? PanCard { get; set; }

    }
    public class EventListResponse
    {
        public EventList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class AssociateImageData
    {
        
        public string? AssociateImage { get; set; }
        public string? Pk_EventBookingId { get; set; }

    }
    public class AssociateImageResponse
    {
        public AssociateImageList? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class AssociateImageList
    {
        public List<AssociateImageData>? EventAssociateImage { get; set; }
    }



}
