module furnace (
    input wire clk,
    input wire reset,
    input wire [7:0] CO,
    input wire [7:0] ethanol,
    input wire [11:0] massflow,
    input wire [15:0] temperature,
    input wire fault,

    output reg fan,
    output reg valve,
    output reg solenoid,
    output reg pump,
    output reg [2:0] state
);

    always @(posedge clk or posedge reset) begin
        if (reset) begin
            state <= 3'b000;
            fan <= 0;
            valve <= 0;
            solenoid <= 0;
            pump <= 0;
        end else begin

            // *** STATE 0: IDLE ***
            if (state == 3'b000) begin
                fan <= 0;
                valve <= 0;
                solenoid <= 0;
                pump <= 0;

                if (massflow > 10 && temperature > 50)
                    state <= 3'b001;
            end

            // *** STATE 1: PRE-HEATING ***
            else if (state == 3'b001) begin
                fan <= 1;
                valve <= 1;

                if (temperature > 200)
                    state <= 3'b010;
            end

            // *** STATE 2: NORMAL BURN ***
            else if (state == 3'b010) begin
                solenoid <= 1;
                pump <= 1;

                if (CO > 100 || ethanol > 50)
                    state <= 3'b011; // go to alarm state
            end

            // *** STATE 3: ALARM ***
            else if (state == 3'b011) begin
                fan <= 1;
                valve <= 0;
                solenoid <= 0;
                pump <= 0;

                if (!fault)
                    state <= 3'b000;
            end

        end
    end

endmodule
