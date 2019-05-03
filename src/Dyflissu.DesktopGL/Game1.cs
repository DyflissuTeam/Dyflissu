using C3.XNA;
using Dyflissu.Physics;
using Dyflissu.Physics.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dyflissu.DesktopGL
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private World world;
        private Body controlledBody;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            world = new World();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            controlledBody = new Body
            {
                Shape = new RectangleShape(10f),
                Mass = 100f,
            };
            
            world.AddBody(controlledBody);
            world.AddBody(new Body
            {
                Mass = 0,
                Shape = new RectangleShape(20f),
                Position = new Primitives.Vector2(0, 50f)
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
            
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                controlledBody.Velocity = new Primitives.Vector2(0, 20f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                controlledBody.Velocity = new Primitives.Vector2(0, -20f);
            }
            else
            {
                controlledBody.Velocity = new Primitives.Vector2(0);
            }

            world.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);

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