USE [master]
GO

/****** Object:  Database [SISPruebaTecnicaBD]    Script Date: 21/12/2022 11:35:21 a. m. ******/
CREATE DATABASE [SISPruebaTecnicaBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SISPruebaTecnicaBD', SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SISPruebaTecnicaBD_log', SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SISPruebaTecnicaBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO