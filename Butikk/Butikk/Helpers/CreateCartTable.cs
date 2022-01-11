using Butikk.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Butikk.Helpers
{
    public class CreateCartTable
    {
        public bool CreateTable()
        {
            try
            {
                var connection = DependencyService.Get<ISQLite>().GetConnection();
                connection.CreateTable<CartItem>();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
