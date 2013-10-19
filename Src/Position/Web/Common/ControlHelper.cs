using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DataAccess.Models;

namespace Web
{
    public static class ControlHelper
    {

        public static List<SelectListItem> GetListItems<T>(List<T> entries) {
            var listItems = new List<SelectListItem>();
            foreach (var entry in entries) {
                listItems.Add(new SelectListItem { Text = GetListItemName(entry), Value = GetListItemValue(entry) });
            }
            return listItems;
        }

        public static string GetListItemName(object o) {
            if (o == null) {
                return string.Empty;
            }

            if (o is Region) {
                return (o as Region).Region_name;
            }
            else if (o is Branch) {
                return (o as Branch).Branch_id.ToString();
            }
            else if (o is Receiver) {
                return (o as Receiver).Receiver_id.ToString();
            }
            else if (o is Position) {
                return (o as Position).Position_desc;
            }
            else {
                return o.ToString();
            }
        }

        public static string GetListItemValue(object o) {
            if (o == null) {
                return string.Empty;
            }

            if (o is Region) {
                return (o as Region).Region_id.ToString();
            }
            else if (o is Branch) {
                return (o as Branch).Branch_id.ToString();
            }
            else if (o is Receiver) {
                return (o as Receiver).Receiver_id.ToString();
            }
            else if (o is Position) {
                return (o as Position).Position_id.ToString();
            }
            else {
                return o.ToString();
            }
        }
    }
}