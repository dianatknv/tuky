import javafx.application.Application;
import javafx.event.EventHandler;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Circle;
import javafx.scene.shape.Line;
import javafx.scene.text.Text;
import javafx.stage.Stage;
import java.util.ArrayList;

public class Main extends Application implements EventHandler<MouseEvent>{
    Pane p, pp;
    int count;
    int dist[];
    int first, second;
    boolean sel;
    boolean u[];
    Text res[];
    TextField tx;
    Button bt, cl, st;
    ArrayList<Circle> v;
    Stage window;
    ArrayList<Integer> [] ed;
    ArrayList<Integer> [] weight;
    Stage w;

    @Override
    public void start(Stage primaryStage){
        pp = new Pane();
        res = new Text[45];
        for (int i = 1; i <= 40; i++)
            res[i] = new Text();
        Scene scc = new Scene(pp, 200, 60);
        w = new Stage();
        w.setScene(scc);
        window = primaryStage;
        tx = new TextField();
        tx.setPromptText("Enter weight");
        tx.setLayoutX(0);
        tx.setLayoutY(0);
        st = new Button("Start");
        st.setOnMouseClicked(this);
        pp.getChildren().add(tx);
        p = new Pane();
        p.getChildren().add(st);
        bt = new Button("ok");
        bt.setLayoutX(120);
        bt.setLayoutY(0);
        bt.setOnMouseClicked(this);
        cl = new Button("Close");
        cl.setLayoutX(150);
        cl.setLayoutY(0);
        cl.setOnMouseClicked(this);
        pp.getChildren().add(bt);
        v = new ArrayList<>();
        dist = new int[45];
        u = new boolean[45];

        v.add(new Circle(0,0,0));
        ed = new ArrayList[45];
        for (int i = 1; i <= 40; i++)
            ed[i] = new ArrayList<Integer>();
        weight = new ArrayList[45];
        for (int i = 1; i <= 40; i++)
            weight[i] = new ArrayList<Integer>();
        sel = false;
        count = 0;
        Scene sc = new Scene(p, 800, 600);
        p.setOnMouseClicked(this);
        primaryStage.setScene(sc);
        primaryStage.show();
        primaryStage.setTitle("Shortest path");
    }

    public static void main(String [] args){
        launch(args);
    }

    public int getPos(int x, int y){
        for (int i = 1; i <= count; i++)
            if ((x - v.get(i).getCenterX()) * (x - v.get(i).getCenterX()) + (y - v.get(i).getCenterY()) * (y - v.get(i).getCenterY()) <= 2209)
                return i;
        return -1;
    }

    public void findShortest(){
        for (int i = 2; i <= count; i++)
            dist[i] = 100100100;
        dist[1] = 0;
        for (int i = 1; i <= count; i++)
            u[i] = false;
        for (int i = 1; i <= count; i++){
            int mn = 100100100, pos = 0;
            for (int j = 1; j <= count; j++)
                if (!u[j])
                    mn = Math.min(mn, dist[j]);
            for (int j = 1; j <= count; j++)
                if (!u[j] && mn == dist[j])
                    pos = j;
            u[pos] = true;
            for (int j = 0; j < ed[pos].size(); j++)
                if (!u[ed[pos].get(j)]){
                    int to = ed[pos].get(j);
                    int ds = weight[pos].get(j);
                    if (dist[pos] + ds < dist[to]){
                        dist[to] = dist[pos] + ds;
                    }
                }
        }
        for (int i = 2; i <= count; i++)
            res[i].setText("");
        for (int i = 2; i <= count; i++){
            int xx, yy;
            xx = (int) v.get(i).getCenterX();
            yy = (int) v.get(i).getCenterY();
            Text tt = new Text("" + dist[i]);
            if (dist[i] == 100100100)
                tt.setText("inf");
            tt.setLayoutX(xx - 3);
            tt.setLayoutY(yy - 25);
            tt.setStroke(Color.BLUEVIOLET);
            res[i] = tt;
            p.getChildren().add(tt);
        }
    }

    @Override
    public void handle(MouseEvent e) {
        if (e.getSource() == st){
            findShortest();
        }
        else if (e.getSource() == bt){
            Text tt = new Text(tx.getText());
            tt.setLayoutX((v.get(first).getCenterX() + v.get(second).getCenterX()) / 2);
            tt.setLayoutY((v.get(first).getCenterY() + v.get(second).getCenterY()) / 2);
            p.getChildren().add(tt);
            weight[first].add(Integer.parseInt(tx.getText()));
            weight[second].add(Integer.parseInt(tx.getText()));
            tx.setText("");
            w.close();
        }
        else if (getPos((int)e.getSceneX(), (int)e.getSceneY()) == -1) {
            Circle cc = new Circle(e.getSceneX(), e.getSceneY(), 23);
            v.add(cc);
            p.getChildren().add(cc);
            count++;
            Text t;
            if (count < 10)
                t = new Text(e.getSceneX() - 3, e.getSceneY() + 3, "" + count);
            else
                t = new Text(e.getSceneX() - 6, e.getSceneY() + 3, "" + count);
            t.setFill(Color.WHITE);
            p.getChildren().add(t);
        }
        else{
            if (!sel){
                sel = true;
                first = getPos((int)e.getSceneX(), (int)e.getSceneY());
            }
            else{
                sel = false;
                int x, y;
                second = getPos((int)e.getSceneX(), (int)e.getSceneY());
                if (first != second) {
                    boolean found = false;
                    for (int i = 0; i < ed[first].size(); i++)
                        if (ed[first].get(i) == second)
                            found = true;
                    if (!found) {
                        w.show();
                        ed[first].add(second);
                        ed[second].add(first);
                        x = (int) v.get(getPos((int) e.getSceneX(), (int) e.getSceneY())).getCenterX();
                        y = (int) v.get(getPos((int) e.getSceneX(), (int) e.getSceneY())).getCenterY();
                        int xx, yy;
                        xx = (int) v.get(first).getCenterX();
                        yy = (int) v.get(first).getCenterY();
                        Line l = new Line(x, y, xx, yy);
                        l.setStrokeWidth(7);
                        l.setStroke(Color.GOLD);
                        p.getChildren().add(l);
                        for (int i = 1; i <= count; i++) {
                            Circle cc = new Circle(v.get(i).getCenterX(), v.get(i).getCenterY(), 23);
                            p.getChildren().add(cc);
                            Text t;
                            if (i < 10)
                                t = new Text(v.get(i).getCenterX() - 3, v.get(i).getCenterY() + 3, "" + i);
                            else
                                t = new Text(v.get(i).getCenterX() - 6, v.get(i).getCenterY() + 3, "" + i);
                            t.setFill(Color.WHITE);
                            p.getChildren().add(t);
                        }
                    }
                }
            }
        }
    }
}
