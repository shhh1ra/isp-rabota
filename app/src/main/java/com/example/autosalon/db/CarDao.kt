package com.example.autosalon.db

import androidx.room.*

@Dao
interface CarDao {

    @Query("""
        SELECT cars.*, brands.name AS brandName
        FROM cars
        JOIN brands ON cars.brandId = brands.id
    """)
    fun getAll(): List<CarWithBrand>

    @Insert
    fun insert(car: CarEntity)

    @Update
    fun update(car: CarEntity)

    @Delete
    fun delete(car: CarEntity)
}
