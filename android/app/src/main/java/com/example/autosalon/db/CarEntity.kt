package com.example.autosalon.db

import androidx.room.Entity
import androidx.room.ForeignKey
import androidx.room.PrimaryKey

@Entity(
    tableName = "cars",
    foreignKeys = [
        ForeignKey(
            entity = BrandEntity::class,
            parentColumns = ["id"],
            childColumns = ["brandId"],
            onDelete = ForeignKey.CASCADE
        )
    ]
)
data class CarEntity(
    @PrimaryKey(autoGenerate = true)
    val id: Int = 0,
    val vin: String,
    val modelYear: Int,
    val color: String,
    val price: Double,
    val brandId: Int
)
