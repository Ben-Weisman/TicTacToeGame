using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class VerificationHandler
    {

        public Boolean VerifyMatrixSizeInput(int i_SizeInput)
        {
            bool flag = false;
            if (int.TryParse(Console.ReadLine(), out int res))
            {
                try
                {
                    m_Manager.MatrixSideSize = res;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return flag;
        }
    }
}