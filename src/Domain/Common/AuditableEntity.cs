﻿using System;

namespace AsukaApi.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
