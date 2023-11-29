using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models;

public class ApiResult<T>
{
    public bool Succeeded { get; set; }
    public T Result { get; set; }
    public List<string> Errors { get; set; }

}
