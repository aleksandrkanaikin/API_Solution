﻿namespace Entities.DataTransferObjects
{
    public class DriverForCreatonDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<CarForCreationDto> Cars { get; set; }
    }
}
