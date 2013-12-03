using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class MonitorPoint
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int? OffsetX { get; set; }
        public int? OffsetY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public long? MonitorContentId { get; set; }
        public decimal? AlarmUp { get; set; }
        public decimal? AlarmDown { get; set; }
        public decimal? RangeUp { get; set; }
        public decimal? RangeDown { get; set; }
        public string Remark { get; set; }

        public MonitorContent MonitorContent { get; set; }

        public int Left {
            get {
                return this.X - (MonitorContent == null ? 0 : MonitorContent.PointerX);
            }
        }

        public int Top {
            get {
                return this.Y - (MonitorContent == null ? 0 : MonitorContent.PointerY);
            }
        }

        public string Image {
            get {
                return MonitorContent == null ? "" : MonitorContent.Image;
            }
        }

        public override string ToString() {
            return string.Format("{0}, Id: {1}, Info: {2}", Name, Id, Information);
        }

        public MonitorPoint() {
            this.Name = string.Empty;
        }

        public MonitorPoint(int id, int left, int top, int contentId, MonitorContent monitorContent)
            : this() {
            this.Id = id;
            this.MonitorContentId = contentId;
            this.X = left + (monitorContent == null ? 0 : monitorContent.PointerX);
            this.Y = top + (monitorContent == null ? 0 : monitorContent.PointerY);
        }
    }
}
