using System;
using System.Drawing;

public class Snowflake
{
    public Point Position { get; private set; }
    private readonly int speed;
    private int formHeight; // Теперь высота формы может изменяться
    private readonly int formWidth;
    private static readonly Random random = new Random();
    public Image SnowflakeImage { get; private set; }

    /// <summary>
    /// Инициализирует параметры снежинки.
    /// </summary>
    public Snowflake(int formWidth, int formHeight, Image snowflakeImage)
    {
        this.formHeight = formHeight;
        this.formWidth = formWidth;
        SnowflakeImage = snowflakeImage;
        Position = new Point(random.Next(0, formWidth), random.Next(-formHeight, 0));
        speed = random.Next(2, 6); // разная скорость падения
    }

    /// <summary>
    /// Обновление позиции снежинки на форме.
    /// </summary>
    public void UpdatePosition()
    {
        Position = new Point(Position.X, Position.Y + speed);
        if (Position.Y > formHeight) // Учитываем новую высоту формы
        {
            Position = new Point(random.Next(0, formWidth), -SnowflakeImage.Height); // Появление сверху
        }
    }

    /// <summary>
    /// Обновление границ формы, чтобы снежинка корректно падала при изменении размера окна.
    /// </summary>
    public void UpdateFormBounds(int newFormWidth, int newFormHeight)
    {
        formHeight = newFormHeight;
    }
}
