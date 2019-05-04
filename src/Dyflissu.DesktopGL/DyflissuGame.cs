using Dyflissu.Physics;
using Dyflissu.Physics.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dyflissu.DesktopGL
{
    public class DyflissuGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private World world;
        private Body controlledBody;
        private MouseState _lastMouseState;

        public DyflissuGame()
        {
            graphics = new GraphicsDeviceManager(this);
            world = new World(new Vector2(0, 1f));
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            controlledBody = new Body
            {
                Shape = new RectangleShape(10f),
                Mass = 1f,
                Position = new Vector2(250f, 250f - 20)
            };
            
            world.AddBody(controlledBody);
            world.AddBody(new Body
            {
                Mass = 0,
                Position = new Vector2(250f, 250f + 20),
                Shape = new RectangleShape(100f, 30f),
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState currentMouseState = Mouse.GetState();

            if (_lastMouseState.LeftButton == ButtonState.Pressed &&
                currentMouseState.LeftButton == ButtonState.Released)
            {
                world.AddBody(new Body
                {
                    Shape = new RectangleShape(10f),
                    Position = new Vector2(currentMouseState.X, currentMouseState.Y)
                });
            }

            Vector2 velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocity = new Vector2(0, 1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                velocity = new Vector2(0, -1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity = new Vector2(-1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity = new Vector2(1, 0);
            }
            else
            {
                velocity = new Vector2(0);
            }

            controlledBody.Velocity = velocity;
            world.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);

            _lastMouseState = currentMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            world.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}