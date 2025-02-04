using System;

namespace Tesla.Business.Interfaces;

public interface IMatematica
{
    Task<float> Sum(float sumando, float sumando_2);
    Task<float> SquareArea(float sideLenght);
}
